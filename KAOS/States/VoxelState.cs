//using KAOS.Interfaces;
//using KAOS.Managers;
//using KAOS.Shapes;
//using KAOS.Utilities;
//using OpenTK.Graphics.OpenGL;
//using System;

//namespace KAOS.States
//{
//    public class VoxelState : IGameObject
//    {
//        double currentRotation = 0;
//        public float length = 1f, height = 1f, width = 1f;

//        StateManager m_stateManager = new StateManager();

//        BufferObjectManager m_bufferObjectManager;
//        BufferObject m_bufferObject;

//        TextureManager m_textureManager = new TextureManager();

//        Cube[][][] m_blocks;

//        public VoxelState(StateManager stateManager)
//        {
//            m_stateManager = stateManager;

//            m_bufferObjectManager = new BufferObjectManager();

//            //LoadSkyBox();

//            GenerateChunk();
//        }

//        private void LoadSkyBox()
//        {
//            string skyboxTexturePath = "Data/Skyboxes/jajlands1/";
//            m_textureManager.LoadSkyTexture("skybox", 
//                new string[] 
//                {
//                    skyboxTexturePath + "jajlands1_ft.jpg",
//                    skyboxTexturePath + "jajlands1_bk.jpg",
//                    skyboxTexturePath + "jajlands1_lf.jpg",
//                    skyboxTexturePath + "jajlands1_rt.jpg",
//                    skyboxTexturePath + "jajlands1_up.jpg",
//                    skyboxTexturePath + "jajlands1_dn.jpg"
//                }
//            );

//        }

//        private void GenerateChunk()
//        {
//            BufferObject tmpVBO = new BufferObject();
//            tmpVBO.PrimitiveType = PrimitiveType.Triangles;
//            m_blocks = new Cube[Utilities.Chunk.CHUNK_SIZE][][];
            
//            for (int x = 0; x < Utilities.Chunk.CHUNK_SIZE; x++)
//            {
//                m_blocks[x] = new Cube[Utilities.Chunk.CHUNK_SIZE][];
//                for (int y = 0; y < Utilities.Chunk.CHUNK_SIZE; y++)
//                {
//                    m_blocks[x][y] = new Cube[Utilities.Chunk.CHUNK_SIZE];
//                    for (int z = 0; z < Utilities.Chunk.CHUNK_SIZE; z++)
//                    {
//                        m_blocks[x][y][z] = new Cube(x, y, z);

//                        if (x == 0 && y == 0 && z == 0) 
//                        { 
//                            tmpVBO.PositionData = m_blocks[x][y][z].Vertices;
//                            tmpVBO.NormalsData = m_blocks[x][y][z].Normals;
//                            tmpVBO.IndicesData = m_blocks[x][y][z].Indices;
//                        }
//                        else
//                        { 
//                            tmpVBO.PositionData = tmpVBO.PositionData.Concat(m_blocks[x][y][z].Vertices);
//                            tmpVBO.NormalsData = tmpVBO.NormalsData.Concat(m_blocks[x][y][z].Normals);
//                            tmpVBO.IndicesData = tmpVBO.IndicesData.Concat(m_blocks[x][y][z].Indices);
//                        }
//                    }
//                }
//            }

//            m_bufferObjectManager.AddBufferObject("chunk-test", tmpVBO, ShaderManager.Get("Voxel").ID);
//            m_bufferObject = m_bufferObjectManager.GetBuffer("chunk-test");
//        }

//        public void Update(float elapsedTime)
//        {
//            currentRotation = 100 * elapsedTime;
//        }

//        public void Render()
//        {
//            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
//            GL.BindVertexArray(m_bufferObject.VaoID);
//            GL.DrawElements(m_bufferObject.PrimitiveType, m_bufferObject.IndicesData.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);                    
//        }
//    }
//}
