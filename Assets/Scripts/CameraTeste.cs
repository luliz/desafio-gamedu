﻿using UnityEngine;


    public class CameraTeste: MonoBehaviour
    {

        public Transform target;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;
        public float yPosRestriction = -1f;
        public float yPosRestrictionUp = 8;
        public float xPlusRestrict = 25f;
        public float xMinusRestrict = -25f;

        private float offsetZ;
        private Vector3 lastTargetPosition;
        private Vector3 currentVelocity;
        private Vector3 lookAheadPos;
        private float nextTimeToSearch = 0;



		public void UpdateCameraPosition () {

			transform.position = new Vector3 (target.position.x, transform.position.y, transform.position.z);
		}
		
        // Use this for initialization
        private void Start()
        {
            lastTargetPosition = target.position;
            offsetZ = (transform.position - target.position).z;
            transform.parent = null;
            

        }

        // Update is called once per frame
        private void Update()
        {
            if (target == null)
            {
                FindPlayer();
                return;
            }

            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.position - lastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
            }
            else
            {
                lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.position + lookAheadPos + Vector3.forward * offsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, damping);

            newPos = new Vector3(Mathf.Clamp(newPos.x, xMinusRestrict, xPlusRestrict), Mathf.Clamp(newPos.y, yPosRestriction, yPosRestrictionUp), newPos.z);

            transform.position = newPos;

            lastTargetPosition = target.position;

        }

        void FindPlayer()
        {
            if (nextTimeToSearch <= Time.time)
            {
                GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
                if (searchResult != null)
                    target = searchResult.transform;
                nextTimeToSearch = Time.time + 0.5f;
            }
        }
    }