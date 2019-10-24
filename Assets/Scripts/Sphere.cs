using UnityEngine;

public class Sphere
{
    ModeleVolumique model;

    public Vector3 position;
    public float radius;

    public Sphere(ModeleVolumique mv, Vector3 pos, float rad)
    {
        model = mv;
        position = pos;
        radius = rad;
    }

    public void Generate()
    {
        float deltaX = model.boxMaxCorner.x - model.boxMinCorner.x;
        float deltaY = model.boxMaxCorner.y - model.boxMinCorner.y;
        float deltaZ = model.boxMaxCorner.z - model.boxMinCorner.z;

        float stepX = deltaX / model.nbCubes;
        float stepY = deltaY / model.nbCubes;
        float stepZ = deltaZ / model.nbCubes;

        for (int i = 0; i < model.nbCubes; i++)
        {
            float x = model.boxMinCorner.x + i * stepX;
            for (int j = 0; j < model.nbCubes; j++)
            {
                float y = model.boxMinCorner.y + j * stepY;
                for (int k = 0; k < model.nbCubes; k++)
                {
                    float z = model.boxMinCorner.z + k * stepZ;
                    Vector3 pos = new Vector3(x, y, z);
                    if (Mathf.Pow(x - position.x, 2) + Mathf.Pow(y - position.y, 2) + Mathf.Pow(z - position.z, 2) - radius * radius < 0)
                    {
                        if (!model.voxelMap.ContainsKey(pos))
                            model.voxelMap.Add(pos, true);
                        else
                            model.voxelMap[pos] = true;
                    }
                    else
                    {
                        if (!model.voxelMap.ContainsKey(pos))
                            model.voxelMap.Add(pos, false);
                        else if (model.voxelMap[pos] != true)
                            model.voxelMap[pos] = false;
                    }
                }
            }
        }
    }

    public void GenerateIntersection(Sphere s)
    {
        for (float x = model.boxMinCorner.x; x < model.boxMaxCorner.x; x++)
            for (float y = model.boxMinCorner.y; y < model.boxMaxCorner.y; y++)
                for (float z = model.boxMinCorner.z; z < model.boxMaxCorner.z; z++)
                {
                    Vector3 pos = new Vector3(x, y, z);
                    if (Mathf.Pow(x - position.x, 2) + Mathf.Pow(y - position.y, 2) + Mathf.Pow(z - position.z, 2) - radius * radius < 0)
                    {
                        if (!model.voxelMap.ContainsKey(pos))
                            model.voxelMap.Add(pos, true);
                        else
                            model.voxelMap[pos] = true;
                    }
                    else
                    {
                        if (!model.voxelMap.ContainsKey(pos))
                            model.voxelMap.Add(pos, false);
                        else if (model.voxelMap[pos] != true)
                            model.voxelMap[pos] = false;
                    }
                }
    }
}
