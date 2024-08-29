using UnityEngine;
using DG.Tweening;
using static UnityEngine.ParticleSystem;

namespace Gamecore
{
    public class PlayerMoveAnimation : MonoBehaviour
    {
        //[SerializeField] private Animator animator;
        [SerializeField] private GameObject[] forwardJetParticles;
        [SerializeField] private GameObject[] backwardJetParticles;
        [SerializeField] private GameObject[] leftJetParticles;
        [SerializeField] private GameObject[] rightJetParticles;

        [SerializeField] private float rotationDuration = 0.3f;
        [SerializeField] private float pitchAngle = 15f;
        [SerializeField] private float yawAngle = 15f;

        private Vector3 previousPosition;
        private string currentAnimation = "Idle";
        private PlayerController playerController;

        void Start()
        {
            playerController = GetComponent<PlayerController>();
            previousPosition = transform.position;
        }

        void Update()
        {
            HandleAnimations();
        }

        private void HandleAnimations()
        {
            
            Vector3 movementDelta = transform.position - previousPosition;
            previousPosition = transform.position;

            SetJetEmission(backwardJetParticles, movementDelta.y > 0.001f);
            SetJetEmission(forwardJetParticles, movementDelta.y < -0.001f);
            SetJetEmission(leftJetParticles, movementDelta.x > 0.001f);
            SetJetEmission(rightJetParticles, movementDelta.x < -0.001f);

            HandleRotation(movementDelta);
        }

        private void HandleRotation(Vector3 movementDelta)
        {
            float targetPitch = Mathf.Clamp(-movementDelta.y * 100 * pitchAngle, -pitchAngle, pitchAngle);
            float targetYaw = Mathf.Clamp(movementDelta.x * 100 * yawAngle, -yawAngle, yawAngle);

            Vector3 targetRotation = new Vector3(targetPitch, targetYaw, 0f);

            transform.DORotate(targetRotation, rotationDuration);
        }

        private void SetJetEmission(GameObject[] jets, bool shouldEmit)
        {
            foreach (var jet in jets)
            {
                if (jet != null)
                {
                    ParticleSystem[] particleSystems = jet.GetComponentsInChildren<ParticleSystem>();
                    foreach (var ps in particleSystems)
                    {
                        var emission = ps.emission;
                        emission.enabled = shouldEmit;
                    }
                }
            }
        }
    }
}
