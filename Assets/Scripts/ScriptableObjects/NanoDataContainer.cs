using System.Collections.Generic;
using UnityEngine;
namespace NanoMesh
{
    [CreateAssetMenu(fileName = "NanoDataContainer", menuName = "NanoMesh/NanoDataContainer")]
    public class NanoDataContainer : ScriptableObject
    {
        public readonly List<Vector3> _sMNormals = new List<Vector3>();
        public readonly List<Vector3> _sMVerts = new List<Vector3>();
        public readonly List<GameObject> _nanoParts = new List<GameObject>();
    }
}
