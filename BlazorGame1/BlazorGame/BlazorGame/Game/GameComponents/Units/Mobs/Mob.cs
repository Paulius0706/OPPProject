using BlazorGame.Game.GameComponents.RendersDecorum.Decorator;
using BlazorGame.Game.GameComponents.RendersDecorum;
using BlazorGame.Game.GameObjects;
using BlazorGame.Game.GameObjects.Factories;
using BlazorGame.Game.GameComponents.States;

namespace BlazorGame.Game.GameComponents.Units.Mobs
{
    public class Mob : Unit
    {
        public const float detectionDistance = 500f;
        public const float maxSpeed = 50f;
        public int id { get; set; }
        public float moveSpeed { get; set; }
        public float damage { get; set; }
        public float maxExperiance { get; set; }
        public float[] tragetDirection = { 1f, 1f };
        public MobState mobState;

        public Mob()
        {
            mobState = new CalmMobState();
            maxHealth = 13f;
            health = maxHealth;
            experiance = 0;
            maxExperiance = 15f;
            moveSpeed = 200f;
            bodyDamage = 1f;
            tragetDirection = new float[2] { 1f,1f};
        }

        public override float CalculateDeathExp()
        {
            return experiance;
        }

        public override void CollisonTrigger(int gameObjectId)
        {
            damageMediator.Collision(this, MainFrame.GameObjects[gameObjectId].AbstractGetComponent<Unit>());

            if( mobState is CalmMobState || mobState.invalid)
            {
                if (MainFrame.GameObjects[gameObjectId] is BulletObject) mobState = new AgressiveMobState(MainFrame.GameObjects[gameObjectId].GetComponent<Bullet>().shooter);
                else mobState = new AgressiveMobState(gameObjectId);
            }
        }

        public override void ConnectionUpdate()
        {
            for (int i = 0; i < renders.renders.Count; i++)
            {
                if (renders.renders[i] is HealthDecorator)
                {
                    healthRenderId = i;
                    healthDecorator.ConnectionUpdate(GameObject);
                }
                if (renders.renders[i] is CircleRender)
                {
                    bodyRenderId = i;
                    bodyRender.ConnectionUpdate(GameObject);
                }
            }
        }

        public override void OnDestroy()
        {
            
        }

        public override void Update()
        {
            //UpdateTarget();
            if (mobState.invalid) mobState = new CalmMobState();
            mobState.Act(this);
            UpdateVelocity();

            bool delete = true;
            // despawn
            foreach (GameObject gameObject in MainFrame.GameObjects.Values)
            {
                if (gameObject.objectType == GameObject.ObjectType.Player
                    && MathF.Abs(gameObject.Position[0] - GameObject.Position[0]) <= Player.DespawnCollectiblesDist
                    && MathF.Abs(gameObject.Position[1] - GameObject.Position[1]) <= Player.DespawnCollectiblesDist)
                {
                    delete = false;
                    break;
                }
            }
            if (delete) { MainFrame.Destroy(GameObject); }
            if (healthDecorator != null) healthDecorator.Update(health, maxHealth);
        }
        public void UpdateTarget()
        {
            float minLen = 1000f;
            foreach (GameObject gameObject in MainFrame.GameObjects.Values)
            {
                if (MathF.Abs(gameObject.Position[0] - GameObject.Position[0]) < detectionDistance
                    && Math.Abs(gameObject.Position[1] - GameObject.Position[1]) < detectionDistance
                    && gameObject is PlayerObject)
                {
                    
                    float[] dist = new float[]
                    {
                        gameObject.Position[0] - GameObject.Position[0],
                        gameObject.Position[1] - GameObject.Position[1]
                    };
                    float len = MathF.Sqrt(dist[0] * dist[0] + dist[1] * dist[1]);
                    if (len < minLen) 
                    {
                        minLen = len;
                        tragetDirection = new float[2] { dist[0] / len, dist[1] / len };
                    }
                }
            }
        }
        public void UpdateVelocity()
        {
            float[] currentVelocity = GameObject.Velocity;

            float x = currentVelocity[0] >  maxSpeed && tragetDirection[0] > 0f ? 0f : tragetDirection[0];
            x =       currentVelocity[0] < -maxSpeed && tragetDirection[0] < 0f ? 0f : tragetDirection[0];
            float y = currentVelocity[1] >  maxSpeed && tragetDirection[1] > 0f ? 0f : tragetDirection[1];
            y =       currentVelocity[1] < -maxSpeed && tragetDirection[1] < 0f ? 0f : tragetDirection[1];
            currentVelocity[0] += x * moveSpeed * MainFrame.DeltaTime;
            currentVelocity[1] += y * moveSpeed * MainFrame.DeltaTime;

            GameObject.Velocity = currentVelocity;
        }
        public Renders renders
        {
            get
            {
                if (GameObject == null) return null;
                if (!GameObject.ContainsComponent<Renders>()) return null;
                return GameObject.GetComponent<Renders>();
            }
            set
            {
                if (GameObject != null && GameObject.ContainsComponent<Renders>()) renders = value;
            }
        }
        private int healthRenderId = -1;
        public HealthDecorator healthDecorator
        {
            get
            {
                if (renders == null || healthRenderId == -1) return null;
                if (renders.renders.Count <= healthRenderId) return null;
                if (renders.renders[healthRenderId] is not HealthDecorator) return null;
                return renders.renders[healthRenderId] as HealthDecorator;
            }
            set
            {
                if (healthRenderId != -1 && renders != null && renders.renders.Count > healthRenderId && renders.renders[healthRenderId] is HealthDecorator)
                    renders.renders[healthRenderId] = value;
            }
        }
        private int bodyRenderId = -1;
        public CircleRender bodyRender
        {
            get
            {
                if (renders == null || bodyRenderId == -1) return null;
                if (renders.renders.Count <= bodyRenderId) return null;
                if (renders.renders[bodyRenderId] is not CircleRender) return null;
                return renders.renders[bodyRenderId] as CircleRender;
            }
            set
            {
                if (bodyRenderId != -1 && renders != null && renders.renders.Count > bodyRenderId && renders.renders[bodyRenderId] is CircleRender)
                    renders.renders[bodyRenderId] = value;
            }
        }
    }
}
