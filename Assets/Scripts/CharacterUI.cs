using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour {

    //and healthbar ref.
    [SerializeField]
    private RectTransform healthBar;

    //Weapone image ref.
    [SerializeField]
    private Image weaponeImage;

    //Ref to lvl UI
    [SerializeField]
    private RectTransform xpBar;
    [SerializeField]
    private Text xpText;
    [SerializeField]
    private Text lvlText;


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
        if (xpBar == null)
        {
            Debug.LogWarning(transform.name + " : Xp bar is not set in inspector! ");
        }
        if (lvlText == null)
        {
            Debug.LogWarning(transform.name + " : Level Text is not set in inspector! ");
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

    public void SetXpBar(int currentXp, int maxXp)
    {
        //Returns health in %%%%%%%%%%%%%% 
        float barScale = ((currentXp * 100) / maxXp) * 0.01f;
        if (barScale < 0)
            barScale = 0;

        xpBar.localScale = new Vector3(barScale, healthBar.localScale.y);
        xpText.text = string.Format("XP {0} / {1} ", currentXp, maxXp);
    }

    public void SetLevelText (int lvl)
    {
        lvlText.text = string.Format("LVL : {0} ", lvl);
    }

    public void SetWeaponImage (Sprite img)
    {
        weaponeImage.sprite = img;
    }
}
