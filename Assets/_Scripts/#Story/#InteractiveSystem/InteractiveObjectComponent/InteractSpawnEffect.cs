using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using System;
using System.Collections;
using DG.Tweening;

namespace ImaginMe
{
	namespace Interactive
	{
		[AddComponentMenu("Pigsssgames/P'Bank story interactive/Particle create")]
		public class InteractSpawnEffect : InteractiveProperties, IPointerClickHandler
		{
			[SerializeField] GameObject particle;
			[SerializeField] int poolSize = 5;
			[HideInInspector] public bool[] isActive;

			public UnityEvent onTrigger;
			
			virtual public void Awake()
			{
				if(particle)PoolManager.Instance.CreatePool(particle,poolSize);
			}
			
			virtual public void OnPointerClick (PointerEventData eventData)
			{
				if(isValid)
				{
					PlayParticle(eventData.pointerCurrentRaycast.worldPosition); // play at collider
					isValid = false;
				}
			}

			public void Init(ParticleSystem _particle)
			{
				particle = _particle.gameObject;
				PoolManager.Instance.CreatePool(particle,poolSize);
			}

			public void PlayParticle(Vector3 position)
			{
				if(onTrigger != null)onTrigger.Invoke();

				GameObject g = PoolManager.Instance.ReuseObject(particle,new Vector3(position.x,position.y,particle.transform.position.z),particle.transform.rotation);
				ParticleSystem particleSys = g.GetComponent<ParticleSystem>();
				particleSys.Stop();
				particleSys.Play();
			}
		}
	}
}
