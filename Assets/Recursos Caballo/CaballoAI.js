 var moveSpeed : float = 5;
 var rotSpeed : float = 10;
 var moveDir : int = 1;
 private var tiempo : int;
 private var tiempo2 : int;
 private var tiempo3 : int;
 var Walk : boolean;
 var anim : Animator;
 var caca : Rigidbody;
 var GOPos : GameObject;
 public var localScale: Vector3;
 var randon = 0.1;

 function start()
 {
 tiempo = Random.Range(0, 1000);
 tiempo2 = Random.Range(0, 1000);
 tiempo3 = Random.Range(0, 1000);
 }

 function Update()
 {
     if(!Physics.Raycast(transform.position, transform.forward, 5))
     {
         transform.Translate(Vector3.forward * moveSpeed * Time.smoothDeltaTime);
     }
     else
     {
         if(Physics.Raycast(transform.position, - transform.right, 1))
         {
             moveDir = 1;
         }
         else if(Physics.Raycast(transform.position, transform.right, 1))
         {
             moveDir = -1;
         }
         transform.Rotate(Vector3.up, 90 * rotSpeed * Time.smoothDeltaTime * moveDir);
     }

 tiempo -= Time.deltaTime * 1;

 tiempo2 -= Time.deltaTime * 1;

 tiempo3 -= Time.deltaTime * 1;

 if (tiempo <= 0){
tiempo = Random.Range(0, 1000);
 }
  if (tiempo2 <= 0){
tiempo2 = Random.Range(0, 1000);
 }

 if (tiempo3 <= 0){
tiempo3 = Random.Range(0, 1000);
 }

 if (tiempo > 500){
 Walk = true;
 moveSpeed = 2;
 anim.SetBool("Walk", true);
 }
  if (tiempo < 300){
 Walk = false;
 moveSpeed = 0;
 anim.SetBool("Walk", false);
 }

  if (tiempo2 < 75 && Walk == true){
  transform.Rotate(Vector3.up, 90 * rotSpeed * Time.smoothDeltaTime * moveDir);
 }
  if (tiempo2 > 925 && Walk == true){
  transform.Rotate(Vector3.up, -90 * rotSpeed * Time.smoothDeltaTime * moveDir);
 }

 if (tiempo3 == 500 && Walk == false){
 cagar();
 }
 if (tiempo3 == 450 && Walk == false){
 cagar();
 }
 if (tiempo3 == 400 && Walk == false){
 cagar();
 }
  }

  function cagar() {
		Instantiate(caca, GOPos.transform.position, GOPos.transform.rotation);
		//transform.localScale += Vector3(1,1,1);
    }