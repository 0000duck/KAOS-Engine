using AWGL.Shapes;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AWGL.Utilities
{
    public class Chunk : IDisposable
    {
        public const int CHUNK_SIZE = 16;
        private Block[][][] m_blocks;
        private List<Block> m_blockStore = new List<Block>(CHUNK_SIZE * 3);

        public Chunk()
        {
            //Create Blocks
            m_blocks = new Block[CHUNK_SIZE][][];
            for (int i = 0; i < CHUNK_SIZE; i++)
            {
                m_blocks[i] = new Block[CHUNK_SIZE][];

                for (int j = 0; j < CHUNK_SIZE; j++)
                {
                    m_blocks[i][j] = new Block[CHUNK_SIZE];
                }
            }
        }

        public void Update(float dt) { }

        public void Render() { }

        public void Dispose()
        {
            // Delete blocks
            for (int i = 0; i < CHUNK_SIZE; i++)
            {
                

                for (int j = 0; j < CHUNK_SIZE; j++)
                {
                    m_blocks[i][j] = null;
                }
                m_blocks[i] = null;
            }
            m_blocks = null;
        }

        public void CreateMesh()
        {
            for (int x = 0; x < CHUNK_SIZE; x++)
            {
                for (int y = 0; y < CHUNK_SIZE; y++)
                {
                    for (int z = 0; z < CHUNK_SIZE; z++)
                    {
                        if (m_blocks[x][y][z].IsActive == false)
                        {
                            // Don't create triangle data for inactive blocks
                            continue;
                        }

                        CreateCube(x, y, z);
                    }
                }
            }
        }

        private void CreateCube(int x, int y, int z)
        {
            float blockSize = 1f;

            
            Vector3 p1 = new Vector3(x - blockSize, y - blockSize, z + blockSize);
            Vector3 p2 = new Vector3(x + blockSize, y - blockSize, z + blockSize);
            Vector3 p3 = new Vector3(x + blockSize, y + blockSize, z + blockSize);
            Vector3 p4 = new Vector3(x + blockSize, y + blockSize, z + blockSize);
            Vector3 p5 = new Vector3(x + blockSize, y - blockSize, z + blockSize);
            Vector3 p6 = new Vector3(x - blockSize, y + blockSize, z - blockSize);
            Vector3 p7 = new Vector3(x - blockSize, y + blockSize, z - blockSize);
            Vector3 p8 = new Vector3(x + blockSize, y + blockSize, z - blockSize);

            Vector3 n1;


            throw new NotImplementedException();
        }
    }
}
