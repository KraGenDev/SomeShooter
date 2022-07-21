class Machine : Weapon
{
     private void OnEnable()
     {
         PlayerInput.Fire1 += Fire;
         PlayerInput.Reload += Reload;
     }
 
     private void OnDisable()
     {
         PlayerInput.Fire1 -= Fire;
         PlayerInput.Reload -= Reload;
     }
}
