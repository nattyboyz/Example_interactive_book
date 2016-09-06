using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.EventSystems;

namespace ImaginMe
{
	namespace Interactive
	{
		[AddComponentMenu("Pigsssgames/P'Bank story interactive/Mix interactive")]
		public class InteractiveObject : MonoBehaviour, IPointerClickHandler {

			bool isValid = true;
			[SerializeField] float delayTime = 1f;
			float time = 0;
			//int stateNo = 0;

			[SerializeField] bool onlyClickOnce;
			[SerializeField] bool alwaysFaceCamera;

			[Header("Sprite")]
			[SerializeField] bool changeSprite;
			[SerializeField] Sprite[] sprites;
			int currentSprite = 0;
			[Header("Particle")]
			[SerializeField] bool playParticle;
			[SerializeField] ParticleSystem particle;
			[SerializeField] Transform targetToSpawn;
			[SerializeField] [Range(-2,2)]float scaleSize = 0f;
			[Header("Animation")]
			[SerializeField] bool playAnimation;
			[Header("Sound")]
			[SerializeField] bool playSound;
			[SerializeField] SfxClip sfx;

			[Header("Star properties")]
			[SerializeField] bool haveStarAttach;
			[SerializeField] GameObject starObject;
			[SerializeField] Transform starTarget;
			[SerializeField] Transform starMaxX;
			[SerializeField] Transform starMinX;
			[SerializeField] Transform starMaxY;
			[SerializeField] Transform starMinY;
			[SerializeField] bool destroyAfterGetStar;
			//[SerializeField] float starMoveToIdleTime = 1f;
//			[SerializeField] float starIdleTime = 2f;
//			[SerializeField] float starMovingTime  = 1f;

//			SpriteRenderer starSprite;
//			ParticleSystem starParticle;
			//[SerializeField] EventTrigger eventTrigger;


//			bool isStarMoving = false;
			
			SpriteRenderer spriteRenderer;
//			EventSystem eventSystem;

			void Awake(){

//				if(EventSystem.current!=null) eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

				if(GetComponent<Collider2D>() && !GetComponent<Rigidbody2D>())
				{
					Debug.LogWarning(gameObject.name + " Don't have rigidbody attach and it may effect performance when you try to move.");
				}
			}

			void Start ()
			{
				//eventTrigger = GetComponent<EventTrigger>();
				//eventTrigger.OnPointerClick += OnClick;
				if(changeSprite)
				{
					if(GetComponent<SpriteRenderer>())
						spriteRenderer = GetComponent<SpriteRenderer>();

					Sprite[] tempSprites = new Sprite[sprites.Length+1];
					for(int i = 0; i<sprites.Length+1;i++)
					{
						if(i==0)
							tempSprites[i] = GetComponent<SpriteRenderer>().sprite;
						else
							tempSprites[i] = sprites[i-1];
					}
					sprites = tempSprites;
				}

				if(playSound)
				{
					//SoundManager.Instance.InitiateClip(sfx,SfxPath.MAIN_PATH);
				}

				if(playParticle && particle)
				{
					GameObject gob = (GameObject)Instantiate(particle.gameObject,targetToSpawn.position,Quaternion.identity);
					particle = gob.GetComponent<ParticleSystem>();
					gob.transform.SetParent(transform);
				}

				if(haveStarAttach && starObject)
				{
//					starSprite = starObject.GetComponent<SpriteRenderer>();
//					starParticle = starObject.GetComponentInChildren<ParticleSystem>();
				}
			}

			void Update()
			{
				if(alwaysFaceCamera) transform.LookAt(Camera.main.transform.position, -Vector3.up);

				if(haveStarAttach && starObject)
				{
					if(!starTarget) starTarget = GameObject.Find("StarTarget").transform;
				}
				if(!isValid)
				{
					time += Time.deltaTime;

					if(time > delayTime){
						time = 0;
						isValid = true;
					}
				}
			}

			IEnumerator ieStarCollected(float movingTime)
			{
				if(starTarget){
					float posX = Random.Range(starMinX.position.x,starMaxX.position.x);
					float posY = Random.Range(starMinY.position.y,starMaxY.position.y);

					starObject.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
					starObject.GetComponent<SpriteRenderer>().sortingOrder = 1000;

					starObject.transform.DOScale(new Vector3(starObject.transform.localScale.x * 1.2f,starObject.transform.localScale.y * 1.2f, .5f),movingTime);
					//starObject.transform.DORotate(new Vector3(starObject.transform.localRotation.x,starObject.transform.localRotation.y,Random.Range(-50f,50f)),.5f);
					starObject.transform.DOMove(new Vector3(posX,posY,starObject.transform.position.z),.5f).OnStart(()=>{
					
						starObject.SetActive(true);
						starObject.GetComponentInChildren<ParticleSystem>().Play();
					}).OnComplete(()=>{
						starObject.GetComponentInChildren<ParticleSystem>().Stop();
						starObject.transform.DORotate(new Vector3(starObject.transform.localRotation.x,starObject.transform.localRotation.y,Random.Range(-90f,90f)),movingTime).SetEase(Ease.InBack);
						starObject.transform.DOScale(new Vector3(starObject.transform.localScale.x / 2,starObject.transform.localScale.y / 2, 1),movingTime).SetEase(Ease.InBack);
						starObject.transform.DOMove(new Vector3(starTarget.position.x,starTarget.position.y,starObject.transform.position.z),movingTime).SetEase(Ease.InOutBack).OnComplete(()=>{

							starObject.GetComponent<SpriteRenderer>().DOFade(0,0.5f).OnStart(()=>{

								starObject.GetComponentInChildren<ParticleSystem>().Stop();
								//EventManager.Instance.QueueEvent(new Ev.OnStarReachTarget());

							}).OnComplete(()=>{

								starObject.transform.position = transform.position;
								starObject.GetComponent<SpriteRenderer>().DOFade(1,0);
								starObject.SetActive(false);
								if (destroyAfterGetStar) gameObject.SetActive(false);

							});
						});
					});
				}
				yield return null;
			}

			public void Click(){
				Debug.LogError("Click");
			}

			public void Select(){
				Debug.LogError("Select");
			}

			public void Drag(){
				Debug.LogError("Drag");

			}

			public void Submit(){

				Debug.LogError("Submit");
			}

			void OnClick(){

				Debug.LogError("On click");
			}

			public void OnPointerClick (PointerEventData eventData){
				TouchDown();
			}

			public void TouchDown()
			{
				{
					if(onlyClickOnce)
					{
						GetComponent<BoxCollider>().enabled = false;
					}
					if(changeSprite)
					{
						ChangeSprite();
					}
					
					if(playParticle)
					{
						PlayParticle();
					}
					
					if(playAnimation)
					{
						PlayAnimation();
					}
					
					if(playSound)
					{
						PlaySound();
					}
					
					if(haveStarAttach)
					{
						StartCoroutine(ieStarCollected(1f));
						//if(EventManager.Instance)EventManager.Instance.QueueEvent(new Ev.OnClickStar());
						//else Debug.Log("EventManager is missing, Don't save star.");
						haveStarAttach = false;
					}
					isValid = false;
	//				isStarMoving = true;	
				}
			}
		/*
			void OnMouseDown()
			{
				TouchDown();
			}
		*/
			void PlaySound()
			{
				SoundManager.Instance.PlaySfx(sfx);
			}

			void ChangeSprite()
			{
				currentSprite++;
				if(currentSprite > sprites.Length-1)
					currentSprite = 0;
				spriteRenderer.sprite = sprites[currentSprite];
			}

			void PlayParticle()
			{
				particle.Stop();
				particle.Play();
			}

			void PlayAnimation(){


			}

		}
	}
}
