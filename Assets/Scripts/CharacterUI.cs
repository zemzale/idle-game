using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour {

    //and healthbar ref.
    [SerializeField]
    private RectTransform healthBar;

    //Weapone image ref.
    [SerializeField]
    private Image weaponeImage;


    void Start ()
    {
        if (healthBar == null)
        {
            Debug.LogWarning(transform.name + " : Health bar is not set in inspector! ");
        }
        if (weaponeImage == null)
        {
            Debug.LogWarning(transform.name + " : Weapon image is not set in inspector! ");
        }
    }

    //Sets healthbar size.
    public void SetHealthBar(int currentHealth, int maxHeath)
    {
        //Returns health in %%%%%%%%%%%%%% 
        float barScale = ((currentHealth * 100) / maxHeath) * 0.01f;
        if (barScale < 0)
            barScale = 0;

        healthBar.localScale = new Vector3(barScale, healthBar.localScale.y);
    }

    public void SetWeaponImage (Sprite img)
    {
        weaponeImage.sprite = img;
    }
}
