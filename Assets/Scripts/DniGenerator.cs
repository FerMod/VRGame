using Garitto.Extensions;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DniGenerator : MonoBehaviour
{
    [Header("UI")]
    public IdUI idUI;

    [Header("Random Values")]
    public string[] firstNames = { "John", "Jane", "Alex", "Emily", "Michael", "Sarah" };
    public string[] surnames = { "Doe", "Smith", "Johnson", "Williams", "Brown", "Jones" };
    public string[] genders = { "M", "F" };
    public string[] nationalities = { "Spanish", "American", "French", "German", "Italian", "British" };
    public Texture[] images;

    private const string dniLetters = "TRWAGMYFPDXBNJZSQVHLCKE";

    public void RandomDni()
    {
        //var dni = GenerateDniDocument();
        //UpdateUI(dni);
    }

    //public IdData GenerateDniDocument() => new()
    //{
    //    number = RandomDniNumber(),
    //    firstSurname = surnames.Random(),
    //    secondSurname = surnames.Random(),
    //    name = firstNames.Random(),
    //    gender = genders.Random(),
    //    nationality = nationalities.Random(),
    //    birthDate = new SerializableDate(new DateTime(Random.Range(1950, 2005), Random.Range(1, 13), Random.Range(1, 29))),
    //    iaps = Random.Range(0, 100000),
    //    expireDate = new SerializableDate(DateTime.Now.AddYears(Random.Range(1, 11))),
    //    image = images.Random(),
    //};

    //private void UpdateUI(IdData dni)
    //{
    //    idUI.numberText.text = dni.number;
    //    idUI.firstSurnameText.text = dni.firstSurname;
    //    idUI.secondSurnameText.text = dni.secondSurname;
    //    idUI.nameText.text = dni.name;
    //    idUI.genderText.text = dni.gender;
    //    idUI.nationalityText.text = dni.nationality;
    //    idUI.birthDateText.text = dni.birthDate.ToFormattedDateTime();
    //    idUI.iapsText.text = dni.iaps.ToString("D5");
    //    idUI.expireDateText.text = dni.expireDate.ToFormattedDateTime();
    //    idUI.image.material.mainTexture = dni.image;
    //}

    private string RandomDniNumber()
    {
        var dniNumber = Random.Range(10000000, 99999999);
        var dniLetter = dniLetters[dniNumber % 23];
        return $"{dniNumber}{dniLetter}";
    }

    private DateTime RandomDate(DateTime startDate, DateTime endDate)
    {
        var range = (endDate - startDate).Days;
        return startDate.AddDays(Random.Range(0, range));
    }
}

