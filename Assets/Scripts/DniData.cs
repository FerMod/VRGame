using System;
using UnityEngine;

[Serializable]
public class DniData
{
    public string number;
    public string firstSurname;
    public string secondSurname;
    public string name;
    public string gender;
    public string nationality;
    public SerializableDate birthDate;
    public int iaps;
    public SerializableDate expireDate;
    public GameObject image;
}

[Serializable]
public class SerializableDate
{
    public int day;
    public int month;
    public int year;

    public SerializableDate(int day, int month, int year)
    {
        this.day = day;
        this.month = month;
        this.year = year;
    }

    public SerializableDate(DateTime dateTime) : this(dateTime.Day, dateTime.Month, dateTime.Year)
    {
    }

    public DateTime ToDateTime() => new(year, month, day);
}
