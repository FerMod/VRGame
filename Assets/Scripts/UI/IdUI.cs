using TMPro;
using UnityEngine;

public class IdUI : MonoBehaviour
{
    public TMP_Text numberText;
    public TMP_Text firstSurnameText;
    public TMP_Text secondSurnameText;
    public TMP_Text nameText;
    public TMP_Text genderText;
    public TMP_Text nationalityText;
    public TMP_Text birthDateText;
    public TMP_Text iapsText;
    public TMP_Text expireDateText;
    public Renderer image;

    [Header("Data")]
    public IdData idData;

    private void Start()
    {
        UpdateUI(idData);
    }

    private void UpdateUI(IdData dni)
    {
        numberText.text = dni.number;
        firstSurnameText.text = dni.firstSurname;
        secondSurnameText.text = dni.secondSurname;
        nameText.text = dni.name;
        genderText.text = dni.gender;
        nationalityText.text = dni.nationality;
        birthDateText.text = dni.birthDate.ToFormattedDateTime();
        iapsText.text = dni.iaps.ToString("D5");
        expireDateText.text = dni.expireDate.ToFormattedDateTime();
        image.material.mainTexture = dni.image;
    }
}
