using System.Collections.Generic;
using UnityEngine;

public class ModeleVolumique : MonoBehaviour
{
    public GameObject cube;
    public int nbCubes = 10;

    [HideInInspector] public Vector3 boxMinCorner;
    [HideInInspector] public Vector3 boxMaxCorner;

    [HideInInspector] public List<Sphere> spheres;

    [HideInInspector] public Dictionary<Vector3, bool> voxelMap;

    private void Start()
    {
        spheres = new List<Sphere>();
        spheres.Add(new Sphere(this, new Vector3(0, 0, 0), 8));
        spheres.Add(new Sphere(this, new Vector3(13, 8, 3), 10));

        ComputeBoundingBoxSize();

        voxelMap = new Dictionary<Vector3, bool>();
        InitBox();

        GenerateSpheres();

        DisplayBox();
    }

    void ComputeBoundingBoxSize()
    {
        boxMinCorner = new Vector3(MinX(), MinY(), MinZ());
        boxMaxCorner = new Vector3(MaxX(), MaxY(), MaxZ());
    }

    void GenerateSpheres()
    {
        foreach (Sphere s in spheres)
            s.Generate();
    }

    void GenerateSphereIntersections(int sphereIndex)
    {
        for (int i = 0; i < spheres.Count; i++)
        {
            if (i != sphereIndex)
            {
                spheres[sphereIndex].GenerateIntersection(spheres[i]);
            }
        }
    }

    void InitBox()
    {
        foreach (Vector3 key in voxelMap.Keys)
        {
            voxelMap[key] = false;
        }
    }

    void DisplayBox()
    {
        foreach (KeyValuePair<Vector3, bool> item in voxelMap)
        {
            voxelMap.TryGetValue(item.Key + new Vector3(0, 1, 0), out bool forward);
            voxelMap.TryGetValue(item.Key + new Vector3(0, -1, 0), out bool backward);

            voxelMap.TryGetValue(item.Key + new Vector3(1, 0, 0), out bool right);
            voxelMap.TryGetValue(item.Key + new Vector3(-1, 0, 0), out bool left);

            voxelMap.TryGetValue(item.Key + new Vector3(0, 0, 1), out bool up);
            voxelMap.TryGetValue(item.Key + new Vector3(0, 0, -1), out bool down);

            //if (!forward || !backward || !right || !left || !up || !down) // affiche mieux, ne fonctionne pas 
            if(item.Value)// affiche les spheres normalement
                Instantiate(cube, item.Key, Quaternion.identity, this.transform);
        }
    }

    float MinX()
    {
        float min = spheres[0].position.x - spheres[0].radius;
        foreach(Sphere s in spheres)
            if (s.position.x - s.radius < min)
                min = s.position.x - s.radius;
        return min;
    }
    float MinY()
    {
        float min = spheres[0].position.y - spheres[0].radius;
        foreach (Sphere s in spheres)
            if (s.position.y - s.radius < min)
                min = s.position.y - s.radius;
        return min;
    }
    float MinZ()
    {
        float min = spheres[0].position.z - spheres[0].radius;
        foreach (Sphere s in spheres)
            if (s.position.z - s.radius < min)
                min = s.position.z - s.radius;
        return min;
    }
    float MaxX()
    {
        float max = spheres[0].position.x + spheres[0].radius;
        foreach (Sphere s in spheres)
            if (s.position.x + s.radius > max)
                max = s.position.x + s.radius;
        return max;
    }
    float MaxY()
    {
        float max = spheres[0].position.y + spheres[0].radius;
        foreach (Sphere s in spheres)
            if (s.position.y + s.radius > max)
                max = s.position.y + s.radius;
        return max;
    }
    float MaxZ()
    {
        float max = spheres[0].position.z + spheres[0].radius;
        foreach (Sphere s in spheres)
            if (s.position.z + s.radius > max)
                max = s.position.z + s.radius;
        return max;
    }
}
