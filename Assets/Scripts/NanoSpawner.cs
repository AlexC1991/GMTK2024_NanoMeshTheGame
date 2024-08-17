using System.Collections.Generic;
using UnityEngine;

namespace NanoMesh
{
    public class NanoSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject nanoPrefab;
        [SerializeField] private GameObject nanoParent;
        [SerializeField] private NanoDataContainer nanoDC;
        [SerializeField] private SkinnedMeshRenderer characterMesh;
        [SerializeField] private Mesh lowerReplacementMesh;
        private float _offsetDistance = 0;
        
        public Transform headBone;
        public Transform leftArmBone;
        public Transform rightArmBone;
        public Transform torsoBone;
        public Transform leftLegBone;
        public Transform rightLegBone;
        public Transform lowerLeftLegBone;
        public Transform lowerRightLegBone;
        public Transform shoulderLeftBone;
        public Transform shoulderRightBone;
        public Transform leftHandBone;
        public Transform rightHandBone;
        public Transform leftFootBone;
        public Transform rightFootBone;
        public Transform leftClavicleBone;
        public Transform rightClavicleBone;
        public Transform neckBone;
        public Transform upperSpineBone;
        public Transform hipBone;
        public Transform eyes;
        public Transform eyeBrows;
        public Transform leftFingerBone;
        public Transform rightFingerBone;
        public Transform leftToesBone;
        public Transform rightToesBone;
        public Transform leftThumbBone;
        public Transform rightThumbBone;
        public Transform leftIndexBone;
        public Transform rightIndexBone;
        public Transform leftAnkleBone;
        public Transform rightAnkleBone;
        
        private Dictionary<string, Transform> boneGroups = new Dictionary<string, Transform>();

        void Start()
        {
            boneGroups.Add("Head", headBone);
            boneGroups.Add("LeftArm", leftArmBone);
            boneGroups.Add("RightArm", rightArmBone);
            boneGroups.Add("Torso", torsoBone);
            boneGroups.Add("LeftLeg", leftLegBone);
            boneGroups.Add("RightLeg", rightLegBone);
            boneGroups.Add("LowerLeftLeg", lowerLeftLegBone);
            boneGroups.Add("LowerRightLeg", lowerRightLegBone);
            boneGroups.Add("ShoulderLeft", shoulderLeftBone);
            boneGroups.Add("ShoulderRight", shoulderRightBone);
            boneGroups.Add("LeftHand", leftHandBone);
            boneGroups.Add("RightHand", rightHandBone);
            boneGroups.Add("LeftFoot", leftFootBone);
            boneGroups.Add("RightFoot", rightFootBone);
            boneGroups.Add("LeftClavicle", leftClavicleBone);
            boneGroups.Add("RightClavicle", rightClavicleBone);
            boneGroups.Add("Neck", neckBone);
            boneGroups.Add("UpperSpine", upperSpineBone);
            boneGroups.Add("Hip", hipBone);
            boneGroups.Add("Eyes", eyes);
            boneGroups.Add("EyeBrows", eyeBrows);
            boneGroups.Add("LeftFinger", leftFingerBone);
            boneGroups.Add("RightFinger", rightFingerBone);
            boneGroups.Add("LeftToes", leftToesBone);
            boneGroups.Add("RightToes", rightToesBone);
            boneGroups.Add("LeftThumb", leftThumbBone);
            boneGroups.Add("RightThumb", rightThumbBone);
            boneGroups.Add("LeftIndex", leftIndexBone);
            boneGroups.Add("RightIndex", rightIndexBone);
            boneGroups.Add("LeftAnkle", leftAnkleBone);
            boneGroups.Add("RightAnkle", rightAnkleBone);
            
            if (characterMesh == null)
            {
                Debug.LogError("Character mesh is not set!");
                return;
            }
            
            foreach (var m in characterMesh.sharedMesh.normals)
            { 
                nanoDC._sMNormals.Add(m);
            }
            
            foreach (var m in characterMesh.sharedMesh.vertices)
            {
                nanoDC._sMVerts.Add(m);
            }

            //StartCoroutine(AssignGameObjectToLocations());

            AssignNanitesToBones();
        }
    
        private void AssignNanitesToBones()
        {
            foreach (var boneEntry in boneGroups)
            {
                string bodyPart = boneEntry.Key;
                Transform bone = boneEntry.Value;

                foreach (var vert in nanoDC._sMVerts)
                {
                    Vector3 worldPos = transform.TransformPoint(vert);
                    if (IsNearBone(worldPos, bone.position))
                    {
                        GameObject nanoInstance = Instantiate(nanoPrefab, worldPos, Quaternion.identity);
                        nanoInstance.transform.parent = bone; // Parent to the appropriate bone
                    }
                }
                
                characterMesh.sharedMesh = lowerReplacementMesh;
            }
        }

        private bool IsNearBone(Vector3 vertPosition, Vector3 bonePosition)
        {
            float distanceThreshold = 0.14f; 
            
            return Vector3.Distance(vertPosition, bonePosition) <= distanceThreshold;
        }

        /*rivate IEnumerator AssignGameObjectToLocations()
        {
            int batchSize = 100;
            int totalVerts = nanoDC._sMVerts.Count;
            int totalNormals = nanoDC._sMNormals.Count;

            for (int v = 0; v < totalVerts; v++)
            {
                Vector3 worldPos = transform.TransformPoint(nanoDC._sMVerts[v]);
                GameObject nanoInstance = Instantiate(nanoPrefab, worldPos, Quaternion.identity);
                nanoInstance.transform.parent = nanoParent.transform;

                if (v % batchSize == 0)
                {
                    yield return null;
                    ;
                }
            }

            for (int i = 0; i < nanoParent.transform.childCount; i++)
            {
                nanoDC._nanoParts.Add(nanoParent.transform.GetChild(i).gameObject);

                if (i % batchSize == 0)
                {
                    yield return null;
                    ;
                }
            }

            for (int i = 0; i < totalNormals; i++)
            {
                Vector3 normal = transform.TransformDirection(nanoDC._sMNormals[i]);
                nanoDC._nanoParts[i].transform.position = transform.TransformPoint( nanoDC._sMVerts[i]) + normal * _offsetDistance;

                if (i % batchSize == 0)
                {
                    yield return null;
                    ;
                }
            }

            characterMesh.sharedMesh = lowerReplacementMesh;
            yield return null;
        }*/
    }
}
