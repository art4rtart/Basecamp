using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UserInfo : MonoBehaviour
{
	public string title;
	[SerializeField] private Subject subject;
	[SerializeField] private Tool tool;
	[SerializeField] private Position userPosition;

	[HideInInspector] public string subjectKR;
	[HideInInspector] public string toolKR;
	[HideInInspector] public string posKR;

	public int userLevel;

	public string currentDate;

	Image image;

	[HideInInspector] public string positions;

	[TextArea(0, 10)]
	public string introduce;

	private void Awake()
	{
		image = GetComponent<Image>();

		subject.ToString();
		Trans(subject, tool, userPosition);

		positions = posKR;
		currentDate = GetCurrentDate() + " " + GetDay(DateTime.Now);
	}

	void Trans(Subject _subject, Tool _tool, Position _position)
	{
		switch (_subject)
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
				toolKR = "DirectX12";
				break;
			case Tool.Unreal:
				toolKR = "언리얼 엔진";
				break;
			case Tool.Unity:
				toolKR = "유니티 엔진";
				break;
			case Tool.OpenGL:
				toolKR = "OpenGL";
				break;
		}

		switch (_position)
		{
			case Position.Client:
				posKR = "클라이언트 프로그래머";
				break;
			case Position.Server:
				posKR = "서버 프로그래머";
				break;
			case Position.Artist:
				posKR = "3D 아티스트";
				break;
			case Position.GameDesign:
				posKR = "게임 기획자";
				break;
		}
	}

	public static string GetCurrentDate()
	{
		return DateTime.Now.ToString(("yyyy.MM.dd"));
	}

	private string GetDay(DateTime _dateTime)
	{
		string dayofweek = "";

		switch (_dateTime.DayOfWeek)
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
