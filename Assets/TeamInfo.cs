using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public enum Subject
{
	SeniorProject,
	ComputerGraphics,
}

public enum Tool
{
	DirectX12,
	Unreal,
	Unity,
}

public enum Position
{
	Client,
	Server,
	Artist,
	GameDesign,
}

public class TeamInfo : MonoBehaviour
{
	[SerializeField] private string title;
	[SerializeField] private Subject subject;
	[SerializeField] private Tool tool;
	[SerializeField] private int recruitNum;
	[SerializeField] private Position[] recriutPosition;

	[HideInInspector] public string subjectKR;
	[HideInInspector] public string toolKR;
	[HideInInspector] public string[] posKR;

	public string currentDate;

	private void Awake()
	{
		posKR = new string[recriutPosition.Length];

		subject.ToString();
		string num = recruitNum.ToString() + "명 ";
		string position = "(";

		Trans(subject, tool, recriutPosition);

		for(int i = 0; i < posKR.Length; i++)
		{
			position += posKR[i].ToString();
			if ( i < posKR.Length - 1) position  +=  ", ";
		}
		position += ")";

		currentDate = GetCurrentDate() + " " + GetDay(DateTime.Now);

		Debug.Log(subjectKR);
		Debug.Log(toolKR);
		Debug.Log(num + position);
	}

	void Trans(Subject _subject, Tool _tool, Position[] _position)
	{
		switch(_subject)
		{
			case Subject.ComputerGraphics:
				subjectKR = "컴퓨터 그래픽스";
				break;
			case Subject.SeniorProject:
				subjectKR = "졸업작품 기획";
				break;
		}

		switch (_tool)
		{
			case Tool.DirectX12:
				toolKR = "다이렉트X";
				break;
			case Tool.Unreal:
				toolKR = "언리얼 엔진";
				break;
			case Tool.Unity:
				toolKR = "유니티 엔진";
				break;
		}

		for(int i = 0; i < _position.Length; i++)
		{
			switch (_position[i])
			{
				case Position.Client:
					posKR[i] = "클라이언트 프로그래머";
					break;
				case Position.Server:
					posKR[i] = "서버 프로그래머";
					break;
				case Position.Artist:
					posKR[i] = "3D 아티스트";
					break;
				case Position.GameDesign:
					posKR[i] = "게임 기획자";
					break;
			}
		}
	}

	public static string GetCurrentDate()
	{
		return DateTime.Now.ToString(("yyyy.MM.dd"));
	}

	private string GetDay(DateTime _dateTime)
	{
		string dayofweek = "";
		
		switch(_dateTime.DayOfWeek)
		{
			case DayOfWeek.Monday:
				dayofweek = "(월)";
				break;
			case DayOfWeek.Tuesday:
				dayofweek = "(화)";
				break;
			case DayOfWeek.Wednesday:
				dayofweek = "(수)";
				break;
			case DayOfWeek.Thursday:
				dayofweek = "(목)";
				break;
			case DayOfWeek.Friday:
				dayofweek = "(금)";
				break;
			case DayOfWeek.Saturday:
				dayofweek = "(토)";
				break;
			case DayOfWeek.Sunday:
				dayofweek = "(일)";
				break;
		}
		return dayofweek;
	}
}
