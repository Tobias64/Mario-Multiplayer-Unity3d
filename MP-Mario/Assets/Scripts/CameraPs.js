#pragma strict
function Start (){
 
     if(networkView.isMine){
         GetComponent(Camera).enabled = true;
     }
     else{
         GetComponent(Camera).enabled = false;
     }
 }
