using Garitto.Extensions;
using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DniGenerator : MonoBehaviour
{
    [Header("UI")]
    public DniUI dniUI;

    [Header("Random Values")]
    public string[] nationalities = { "Spanish", "American", "French", "German", "Italian", "British" };
    public string[] firstNames = { "John", "Jane", "Alex", "Emily", "Michael", "Sarah" };
    public string[] surnames = { "Doe", "Smith", "Johnson", "Williams", "Brown", "Jones" };
    public string[] genders = { "M", "F" };

    private const string dniLetters = "TRWAGMYFPDXBNJZSQVHLCKE";

    public void RandomDni()
    {
        var dni = GenerateDniDocument();
        UpdateUI(dni);
    }

    public DniData GenerateDniDocument() => new()
    {
        number = RandomDniNumber(),
        firstSurname = surnames.Random(),
        secondSurname = surnames.Random(),
        name = firstNames.Random(),
        gender = genders.Random(),
        nationality = nationalities.Random(),
        birthDate = new SerializableDate(new DateTime(Random.Range(1950, 2005), Random.Range(1, 13), Random.Range(1, 29))),
        iaps = Random.Range(0, 100000),
        expireDate = new SerializableDate(DateTime.Now.AddYears(Random.Range(1, 11))),
        image = null // Assign a default or random image if needed
    };

    private void UpdateUI(DniData dni)
    {
        dniUI.numberText.text = dni.number;
        dniUI.firstSurnameText.text = dni.firstSurname;
        dniUI.secondSurnameText.text = dni.secondSurname;
        dniUI.nameText.text = dni.name;
        dniUI.genderText.text = dni.gender;
        dniUI.nationalityText.text = dni.nationality;
        dniUI.birthDateText.text = dni.birthDate.ToDateTime().ToString("dd/MM/yyyy");
        dniUI.iapsText.text = dni.iaps.ToString("D5");
        dniUI.expireDateText.text = dni.expireDate.ToDateTime().ToString("dd/MM/yyyy");
    }

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

