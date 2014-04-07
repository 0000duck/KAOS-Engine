using Assimp;
using Assimp.Configs;
using KAOS.Interfaces;
using KAOS.Managers;
using KAOS.Utilities;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KAOS.States
{
    public class ModelState : AbstractState
    {
        private Scene m_model;
        private Vector3 m_sceneCenter, m_sceneMin, m_sceneMax;
        private float m_angle;
        private int m_displayList;
        private int m_texId;

        #region Core 3.3 Update

        // Information to render each assimp node
        private struct MyMesh
        {
            uint vao;
            uint texIndex;
            uint uniformBlockIndex;
            int numFaces;
        }

        //std::vector<struct MyMesh> myMeshes;

        // This is for a shader uniform block
        struct MyMaterial{

            float[] diffuse;
            float[] ambient;
            float[] specular;
            float[] emissive;
	        float shininess;
	        int texCount;
        };

        // Model Matrix (part of the OpenGL Model View Matrix)
        float[] modelMatrix = new float[16];

        // For push and pop matrix
        //std::vector<float *> matrixStack;

        // Vertex Attribute Locations
        uint vertexLoc=0, normalLoc=1, texCoordLoc=2;

        // Uniform Bindings Points
        uint matricesUniLoc = 1, materialUniLoc = 2;

        // The sampler uniform for textured models
        // we are assuming a single texture so this will
        //always be texture unit 0
        uint texUnit = 0;

        // Uniform Buffer for Matrices
        // this buffer will contain 3 matrices: projection, view and model
        // each matrix is a float array with 16 components
        uint matricesUniBuffer;
        int MatricesUniBufferSize = sizeof(float) * 16 * 3;
        int ProjMatrixOffset = 0;
        int ViewMatrixOffset = sizeof(float) * 16;
        int ModelMatrixOffset = sizeof(float) * 16 * 2;
        int MatrixSize = sizeof(float) * 16;

        // Program and Shader Identifiers
        uint program, vertexShader, fragmentShader;

        // Shader Names
        string vertexFileName = "Data\\Shaders\\assimp-vs.glsl";
        string fragmentFileName = "Data\\Shaders\\assimp-fs.glsl";

        // Create an instance of the Importer class
        Assimp.AssimpContext importer;

        // the global Assimp scene object
        Assimp.Scene scene;

        // scale factor for the model to fit in the window
        float scaleFactor;


        // images / texture
        // map image filenames to textureIds
        // pointer to texture Array
        //std::map<std::string, GLuint> textureIdMap;	

        // Replace the model name by your model's filename
        //static const std::string modelname = "bench.obj";


        // Camera Position
        float camX = 0, camY = 0, camZ = 5;

        // Mouse Tracking Variables
        int startX, startY, tracking = 0;

        // Camera Spherical Coordinates
        float alpha = 0.0f, beta = 0.0f;
        float r = 5.0f;

        //#define M_PI       3.14159265358979323846f

        private static float DegToRad(float degrees) 
        { 
	        return (float)(degrees * (3.14159265358979323846f / 180.0f));
        }

        // Frame counting and FPS computation
        //long time,timebase = 0,frame = 0;
        //char s[32];

        // ----------------------------------------------------
        // VECTOR STUFF
        //


        // res = a cross b;
        void crossProduct(float[] a, float[] b, float[] res)
        {

            res[0] = a[1] * b[2] - b[1] * a[2];
            res[1] = a[2] * b[0] - b[2] * a[0];
            res[2] = a[0] * b[1] - b[0] * a[1];
        }


        // Normalize a vec3
        void normalize(float[] a)
        {

            float mag = (float) Math.Sqrt(a[0] * a[0] + a[1] * a[1] + a[2] * a[2]);

            a[0] /= mag;
            a[1] /= mag;
            a[2] /= mag;
        }


        // ----------------------------------------------------
        // MATRIX STUFF
        //

        // Push and Pop for modelMatrix

        void pushMatrix()
        {

            //float* aux = (float*)malloc(sizeof(float) * 16);
            //memcpy(aux, modelMatrix, sizeof(float) * 16);
            //matrixStack.push_back(aux);
        }

        void popMatrix()
        {

            //float* m = matrixStack[matrixStack.size() - 1];
            //memcpy(modelMatrix, m, sizeof(float) * 16);
            //matrixStack.pop_back();
            //free(m);
        }

        // sets the square matrix mat to the identity matrix,
        // size refers to the number of rows (or columns)
        void setIdentityMatrix(float[] mat, int size)
        {

            // fill matrix with 0s
            for (int i = 0; i < size * size; ++i)
                mat[i] = 0.0f;

            // fill diagonal with 1s
            for (int i = 0; i < size; ++i)
                mat[i + i * size] = 1.0f;
        }


        //
        // a = a * b;
        //
        void multMatrix(float[] a, float[] b) {

            float[] res = new float[16];

            for (int i = 0; i < 4; ++i) {
                for (int j = 0; j < 4; ++j) {
                    res[j*4 + i] = 0.0f;
                    for (int k = 0; k < 4; ++k) {
                        res[j*4 + i] += a[k*4 + i] * b[j*4 + k]; 
                    }
                }
            }
            Array.Copy(a, res, 16 * sizeof(float));

        }


        // Defines a transformation matrix mat with a translation
        void setTranslationMatrix(float[] mat, float x, float y, float z)
        {

            setIdentityMatrix(mat, 4);
            mat[12] = x;
            mat[13] = y;
            mat[14] = z;
        }

        // Defines a transformation matrix mat with a scale
        void setScaleMatrix(float[] mat, float sx, float sy, float sz)
        {

            setIdentityMatrix(mat, 4);
            mat[0] = sx;
            mat[5] = sy;
            mat[10] = sz;
        }

        // Defines a transformation matrix mat with a rotation 
        // angle alpha and a rotation axis (x,y,z)
        void setRotationMatrix(float[] mat, float angle, float x, float y, float z)
        {

            float radAngle = DegToRad(angle);
            float co = (float)Math.Cos(radAngle);
            float si = (float)Math.Sin(radAngle);
            float x2 = x * x;
            float y2 = y * y;
            float z2 = z * z;

            mat[0] = x2 + (y2 + z2) * co;
            mat[4] = x * y * (1 - co) - z * si;
            mat[8] = x * z * (1 - co) + y * si;
            mat[12] = 0.0f;

            mat[1] = x * y * (1 - co) + z * si;
            mat[5] = y2 + (x2 + z2) * co;
            mat[9] = y * z * (1 - co) - x * si;
            mat[13] = 0.0f;

            mat[2] = x * z * (1 - co) - y * si;
            mat[6] = y * z * (1 - co) + x * si;
            mat[10] = z2 + (x2 + y2) * co;
            mat[14] = 0.0f;

            mat[3] = 0.0f;
            mat[7] = 0.0f;
            mat[11] = 0.0f;
            mat[15] = 1.0f;

        }

        // ----------------------------------------------------
        // Model Matrix 
        //
        // Copies the modelMatrix to the uniform buffer


        void setModelMatrix()
        {

            GL.BindBuffer(BufferTarget.UniformBuffer, matricesUniBuffer);
            GL.BufferSubData(BufferTarget.UniformBuffer, new IntPtr(ModelMatrixOffset), new IntPtr(MatrixSize), modelMatrix);
            GL.BindBuffer(BufferTarget.UniformBuffer, 0);

        }

        // The equivalent to glTranslate applied to the model matrix
        void translate(float x, float y, float z) {

	        float[] aux = new float[16];

	        setTranslationMatrix(aux,x,y,z);
	        multMatrix(modelMatrix,aux);
	        setModelMatrix();
        }

        // The equivalent to glRotate applied to the model matrix
        void rotate(float angle, float x, float y, float z) {

	        float[] aux = new float[16];

	        setRotationMatrix(aux,angle,x,y,z);
	        multMatrix(modelMatrix,aux);
	        setModelMatrix();
        }

        // The equivalent to glScale applied to the model matrix
        void scale(float x, float y, float z) {

	        float[] aux = new float[16];

	        setScaleMatrix(aux,x,y,z);
	        multMatrix(modelMatrix,aux);
	        setModelMatrix();
        }

        // ----------------------------------------------------
        // Projection Matrix 
        //
        // Computes the projection Matrix and stores it in the uniform buffer

        void buildProjectionMatrix(float fov, float ratio, float nearp, float farp) {

	        float[] projMatrix = new float[16];

            float f = 1.0f / (float)Math.Tan(fov * (3.14159265358979323846f / 360.0f));

	        setIdentityMatrix(projMatrix,4);

	        projMatrix[0] = f / ratio;
	        projMatrix[1 * 4 + 1] = f;
	        projMatrix[2 * 4 + 2] = (farp + nearp) / (nearp - farp);
	        projMatrix[3 * 4 + 2] = (2.0f * farp * nearp) / (nearp - farp);
	        projMatrix[2 * 4 + 3] = -1.0f;
	        projMatrix[3 * 4 + 3] = 0.0f;

	        GL.BindBuffer(BufferTarget.UniformBuffer,matricesUniBuffer);
	        GL.BufferSubData(BufferTarget.UniformBuffer, new IntPtr(ProjMatrixOffset), new IntPtr(MatrixSize), projMatrix);
	        GL.BindBuffer(BufferTarget.UniformBuffer,0);

        }


        // ----------------------------------------------------
        // View Matrix
        //
        // Computes the viewMatrix and stores it in the uniform buffer
        //
        // note: it assumes the camera is not tilted, 
        // i.e. a vertical up vector along the Y axis (remember gluLookAt?)
        //

        void setCamera(float posX, float posY, float posZ,
                        float lookAtX, float lookAtY, float lookAtZ) {

	        float[] dir = new float[3];
            float[] right = new float[3];
            float[] up = new float[3];

	        up[0] = 0.0f;	up[1] = 1.0f;	up[2] = 0.0f;

	        dir[0] =  (lookAtX - posX);
	        dir[1] =  (lookAtY - posY);
	        dir[2] =  (lookAtZ - posZ);
	        normalize(dir);

	        crossProduct(dir,up,right);
	        normalize(right);

	        crossProduct(right,dir,up);
	        normalize(up);

            float[] viewMatrix = new float[16];
            float[] aux = new float[16];

	        viewMatrix[0]  = right[0];
	        viewMatrix[4]  = right[1];
	        viewMatrix[8]  = right[2];
	        viewMatrix[12] = 0.0f;

	        viewMatrix[1]  = up[0];
	        viewMatrix[5]  = up[1];
	        viewMatrix[9]  = up[2];
	        viewMatrix[13] = 0.0f;

	        viewMatrix[2]  = -dir[0];
	        viewMatrix[6]  = -dir[1];
	        viewMatrix[10] = -dir[2];
	        viewMatrix[14] =  0.0f;

	        viewMatrix[3]  = 0.0f;
	        viewMatrix[7]  = 0.0f;
	        viewMatrix[11] = 0.0f;
	        viewMatrix[15] = 1.0f;

	        setTranslationMatrix(aux, -posX, -posY, -posZ);

	        multMatrix(viewMatrix, aux);
	
	        GL.BindBuffer(BufferTarget.UniformBuffer, matricesUniBuffer);
	        GL.BufferSubData(BufferTarget.UniformBuffer, new IntPtr(ViewMatrixOffset), new IntPtr(MatrixSize), viewMatrix);
	        GL.BindBuffer(BufferTarget.UniformBuffer, 0);
        }


        // ----------------------------------------------------------------------------

        private int aisgl_min(int x, int y) 
        {
            return x < y ? x : y;
        }

        private int aisgl_max(int x, int y)
        {
            return y > x ? y : x;
        }

        void get_bounding_box_for_node (Assimp.Node nd, Vector3 min, Vector3 max)
	
        {
            //Matrix4x4 prev;
            //uint n = 0, t;

            //for (; n < nd.MeshCount; ++n) {
            //    Assimp.Mesh mesh = scene.Meshes[nd.Children.IndexOf(n)];  //mMeshes[nd->mMeshes[n]];    //////////////////////////////////////////////////////////////////////////
            //    for (t = 0; t < mesh->mNumVertices; ++t) {

            //        aiVector3D tmp = mesh->mVertices[t];

            //        min->x = aisgl_min(min->x,tmp.x);
            //        min->y = aisgl_min(min->y,tmp.y);
            //        min->z = aisgl_min(min->z,tmp.z);

            //        max->x = aisgl_max(max->x,tmp.x);
            //        max->y = aisgl_max(max->y,tmp.y);
            //        max->z = aisgl_max(max->z,tmp.z);
            //    }
            //}

            //for (n = 0; n < nd->mNumChildren; ++n) {
            //    get_bounding_box_for_node(nd->mChildren[n],min,max);
            //}
        }

        #endregion

        #region Pre Core 3.3 Update

        public ModelState(StateManager stateManager)
        {
            m_bufferManager = new VertexBufferManager();
            m_stateManager = stateManager;
            m_textureManager = new TextureManager();
            String fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), 
                "Content\\Models\\Characters\\bunny\\reconstruction\\bun_zipper.ply");

            AssimpContext importer = new AssimpContext();
            importer.SetConfig(new NormalSmoothingAngleConfig(66.0f));
            m_model = importer.ImportFile(fileName, PostProcessPreset.TargetRealTimeMaximumQuality);

            if (m_model == null)
            {
                Logger.WriteLine("Import failed.");
            }

            ComputeBoundingBox();
        }

        private void ComputeBoundingBox()
        {
            m_sceneMin = new Vector3(1e10f, 1e10f, 1e10f);
            m_sceneMax = new Vector3(-1e10f, -1e10f, -1e10f);
            Matrix4 identity = Matrix4.Identity;

            ComputeBoundingBox(m_model.RootNode, ref m_sceneMin, ref m_sceneMax, ref identity);

            m_sceneCenter.X = (m_sceneMin.X + m_sceneMax.X) / 2.0f;
            m_sceneCenter.Y = (m_sceneMin.Y + m_sceneMax.Y) / 2.0f;
            m_sceneCenter.Z = (m_sceneMin.Z + m_sceneMax.Z) / 2.0f;
        }

        private void ComputeBoundingBox(Node node, ref Vector3 min, ref Vector3 max, ref Matrix4 trafo)
        {
            Matrix4 prev = trafo;
            trafo = Matrix4.Mult(prev, FromMatrix(node.Transform));

            if (node.HasMeshes)
            {
                foreach (int index in node.MeshIndices)
                {
                    Mesh mesh = m_model.Meshes[index];
                    for (int i = 0; i < mesh.VertexCount; i++)
                    {
                        Vector3 tmp = FromVector(mesh.Vertices[i]);
                        Vector3.Transform(ref tmp, ref trafo, out tmp);

                        min.X = Math.Min(min.X, tmp.X);
                        min.Y = Math.Min(min.Y, tmp.Y);
                        min.Z = Math.Min(min.Z, tmp.Z);

                        max.X = Math.Max(max.X, tmp.X);
                        max.Y = Math.Max(max.Y, tmp.Y);
                        max.Z = Math.Max(max.Z, tmp.Z);
                    }
                }
            }

            for (int i = 0; i < node.ChildCount; i++)
            {
                ComputeBoundingBox(node.Children[i], ref min, ref max, ref trafo);
            }
            trafo = prev;
        }

        public override void Update(float elapsedTime, float aspect)
        {
            m_angle += 25f * elapsedTime;
            if (m_angle > 360)
            {
                m_angle = 0.0f;
            }
        }

        public override void Render()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Normalize);
            GL.FrontFace(FrontFaceDirection.Ccw);

            GL.MatrixMode(MatrixMode.Modelview);
            Matrix4 lookat = Matrix4.LookAt(0, 5, 5, 0, 0, 0, 0, 1, 0);
            GL.LoadMatrix(ref lookat);

            GL.Rotate(m_angle, 0.0f, 1.0f, 0.0f);

            float tmp = m_sceneMax.X - m_sceneMin.X;
            tmp = Math.Max(m_sceneMax.Y - m_sceneMin.Y, tmp);
            tmp = Math.Max(m_sceneMax.Z - m_sceneMin.Z, tmp);
            tmp = 1.0f / tmp;
            GL.Scale(tmp * 2, tmp * 2, tmp * 2);

            GL.Translate(-m_sceneCenter);

            if (m_displayList == 0)
            {
                m_displayList = GL.GenLists(1);
                GL.NewList(m_displayList, ListMode.Compile);
                RecursiveRender(m_model, m_model.RootNode);
                GL.EndList();
            }

            GL.CallList(m_displayList);
        }

        private void RecursiveRender(Scene scene, Node node)
        {
            Matrix4 m = FromMatrix(node.Transform);
            m.Transpose();
            GL.PushMatrix();
            GL.MultMatrix(ref m);

            if (node.HasMeshes)
            {
                foreach (int index in node.MeshIndices)
                {
                    Mesh mesh = scene.Meshes[index];
                    ApplyMaterial(scene.Materials[mesh.MaterialIndex]);

                    if (mesh.HasNormals)
                    {
                        GL.Enable(EnableCap.Lighting);
                    }
                    else
                    {
                        GL.Disable(EnableCap.Lighting);
                    }

                    bool hasColors = mesh.HasVertexColors(0);
                    if (hasColors)
                    {
                        GL.Enable(EnableCap.ColorMaterial);
                    }
                    else
                    {
                        GL.Disable(EnableCap.ColorMaterial);
                    }

                    bool hasTexCoords = mesh.HasTextureCoords(0);

                    foreach (Face face in mesh.Faces)
                    {
                        BeginMode faceMode;
                        switch (face.IndexCount)
                        {
                            case 1:
                                faceMode = BeginMode.Points;
                                break;
                            case 2:
                                faceMode = BeginMode.Lines;
                                break;
                            case 3:
                                faceMode = BeginMode.Triangles;
                                break;
                            default:
                                faceMode = BeginMode.Polygon;
                                break;
                        }

                        GL.Begin(faceMode);
                        for (int i = 0; i < face.IndexCount; i++)
                        {
                            int indice = face.Indices[i];
                            if (hasColors)
                            {
                                Color4 vertColor = FromColor(mesh.VertexColorChannels[0][indice]);
                            }
                            if (mesh.HasNormals)
                            {
                                Vector3 normal = FromVector(mesh.Normals[indice]);
                                GL.Normal3(normal);
                            }
                            if (hasTexCoords)
                            {
                                Vector3 uvw = FromVector(mesh.TextureCoordinateChannels[0][indice]);
                                GL.TexCoord2(uvw.X, 1 - uvw.Y);
                            }
                            Vector3 pos = FromVector(mesh.Vertices[indice]);
                            GL.Vertex3(pos);
                        }
                        GL.End();
                    }
                }
            }

            for (int i = 0; i < node.ChildCount; i++)
            {
                RecursiveRender(m_model, node.Children[i]);
            }
        }

        private void LoadTexture(String fileName)
        {
            fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileName);
            if (!File.Exists(fileName))
            {
                return;
            }
            Bitmap textureBitmap = new Bitmap(fileName);
            BitmapData TextureData =
                            textureBitmap.LockBits(
                            new System.Drawing.Rectangle(0, 0, textureBitmap.Width, textureBitmap.Height),
                            System.Drawing.Imaging.ImageLockMode.ReadOnly,
                            System.Drawing.Imaging.PixelFormat.Format24bppRgb
                    );
            m_texId = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, m_texId);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, textureBitmap.Width, textureBitmap.Height, 0,
                    OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, TextureData.Scan0);
            textureBitmap.UnlockBits(TextureData);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }

        private void ApplyMaterial(Material mat)
        {
            if (mat.GetMaterialTextureCount(TextureType.Diffuse) > 0)
            {
                TextureSlot tex;
                if (mat.GetMaterialTexture(TextureType.Diffuse, 0, out tex))
                    LoadTexture(tex.FilePath);
            }

            Color4 color = new Color4(.8f, .8f, .8f, 1.0f);
            if (mat.HasColorDiffuse)
            {
                // color = FromColor(mat.ColorDiffuse);
            }
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, color);

            color = new Color4(0, 0, 0, 1.0f);
            if (mat.HasColorSpecular)
            {
                color = FromColor(mat.ColorSpecular);
            }
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, color);

            color = new Color4(.2f, .2f, .2f, 1.0f);
            if (mat.HasColorAmbient)
            {
                color = FromColor(mat.ColorAmbient);
            }
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, color);

            color = new Color4(0, 0, 0, 1.0f);
            if (mat.HasColorEmissive)
            {
                color = FromColor(mat.ColorEmissive);
            }
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, color);

            float shininess = 1;
            float strength = 1;
            if (mat.HasShininess)
            {
                shininess = mat.Shininess;
            }
            if (mat.HasShininessStrength)
            {
                strength = mat.ShininessStrength;
            }

            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, shininess * strength);
        }

        private Matrix4 FromMatrix(Matrix4x4 mat)
        {
            Matrix4 m = new Matrix4();
            m.M11 = mat.A1;
            m.M12 = mat.A2;
            m.M13 = mat.A3;
            m.M14 = mat.A4;
            m.M21 = mat.B1;
            m.M22 = mat.B2;
            m.M23 = mat.B3;
            m.M24 = mat.B4;
            m.M31 = mat.C1;
            m.M32 = mat.C2;
            m.M33 = mat.C3;
            m.M34 = mat.C4;
            m.M41 = mat.D1;
            m.M42 = mat.D2;
            m.M43 = mat.D3;
            m.M44 = mat.D4;
            return m;
        }

        private Vector3 FromVector(Vector3D vec)
        {
            Vector3 v;
            v.X = vec.X;
            v.Y = vec.Y;
            v.Z = vec.Z;
            return v;
        }

        private Color4 FromColor(Color4D color)
        {
            Color4 c;
            c.R = color.R;
            c.G = color.G;
            c.B = color.B;
            c.A = color.A;
            return c;
        }

        #endregion
    }
}