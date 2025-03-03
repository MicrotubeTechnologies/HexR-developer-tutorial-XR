using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using HaptGlove;

public class ObjectLauncher : MonoBehaviour
    {
        public Transform SpawnPoint;
    public GameObject GameComponent;
        public ProjectileBase ObjectToSpawn;
        public bool IsAutoSpawn = true;
        public float LaunchRate = 0.6f;

        public AudioClip LaunchingClip;
    public HaptGloveHandler gloveHandler;
    float m_LastLaunch = 0.5f;

        Queue<ProjectileBase> m_ProjectilesPool = new Queue<ProjectileBase>();

        void Awake()
        {
            enabled = false;

            for (int i = 0; i < 32; ++i)
            {
                var newObj = Instantiate(ObjectToSpawn, SpawnPoint.position, SpawnPoint.rotation);
                newObj.gameObject.SetActive(false);
                m_ProjectilesPool.Enqueue(newObj);
            }
        }

        public void Activated()
        {
         if(GameComponent.activeInHierarchy)
        {
            //if this is auto spawn regularly, we enable the script so the update is called.
            if (IsAutoSpawn)
            {
                enabled = true;
                m_LastLaunch = LaunchRate;
            }
            SendHaptics();
            Launch();
        }

        }

        public void Deactivated()
        {
            enabled = false;
            StopHaptic();
        }

    void Update()
    {

            if (Input.GetKeyDown(KeyCode.L))
            {
                Activated();
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                Deactivated();
            }

            if (m_LastLaunch > 0.0f)
            {
                m_LastLaunch -= Time.deltaTime;

                if (m_LastLaunch <= 0.0f)
                {
                    Launch();
                    m_LastLaunch = LaunchRate;
                }
            }
    }

        void Launch()
        {
            var p = m_ProjectilesPool.Dequeue();
            p.gameObject.SetActive(true);
            p.transform.position = SpawnPoint.position;
            p.Launched(SpawnPoint.transform.forward, this);

            SFXPlayer.Instance.PlaySFX(LaunchingClip, SpawnPoint.position, new SFXPlayer.PlayParameters()
            {
                Pitch = Random.Range(0.9f, 1.2f),
                Volume = 1.0f,
                SourceID = -999
            });
        }

        public void ReturnProjectile(ProjectileBase proj)
        {
            m_ProjectilesPool.Enqueue(proj);
        }
    public void SendHaptics()
    {
        byte[][] clutchStates = new byte[][] { new byte[] { 1, 0 } };

        byte[][] valveTimings = new byte[][] {new byte[] { 3, 10 }};

        //byte[] clutchState = { 1, 0 };
        byte[] btData = gloveHandler.haptics.ApplyHaptics(clutchStates, 60, false);
        //byte[] btData = gloveHandler.haptics.ApplyHapticsWithTiming(clutchStates, valveTimings, false);
        gloveHandler.BTSend(btData);
    }

    public void StopHaptic()
    {
        byte[][] clutchStates = new byte[][] { new byte[] { 1, 2 }};

        //byte[] clutchState = { 1, 0 };
        byte[] btData = gloveHandler.haptics.ApplyHaptics(clutchStates, 60, false);
        gloveHandler.BTSend(btData);

    }
}

    public abstract class ProjectileBase : MonoBehaviour
    {
        public abstract void Launched(Vector3 direction, ObjectLauncher launcher);
    }
