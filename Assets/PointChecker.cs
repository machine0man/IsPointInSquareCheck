using System;
using UnityEngine;

namespace Nature
{
	public class PointChecker : MonoBehaviour
	{

		[SerializeField] Vector2 m_Point;
		[SerializeField] Vector2 m_squareCenter;
		[SerializeField] float m_squareSideLength;
		[SerializeField] float m_squareRotationAngle;
		[SerializeField] Transform m_transSquare;


		Vector3 m_rotatedPoint; //for drawing

		private void Update()
		{
			DrawSquare();
			CheckPosition();
		}
		void DrawSquare()
		{
			m_transSquare.position = m_squareCenter;
			m_transSquare.rotation = Quaternion.Euler(0, 0, m_squareRotationAngle);
		}
		private void CheckPosition()
		{
			if (IsInsideSquare(m_Point, m_squareCenter, m_squareSideLength, m_squareRotationAngle))
			{
				Debug.Log("Yes");
			}
			else
				Debug.Log("No");
		}
		private bool IsInsideSquare(Vector2 a_point, Vector2 a_squareCenter, float a_squareLength, float a_squareRotationAngle)
		{

			//  ( rCosx , rSinx   )
			// rotation by t
			//  ( rCos(x+t), rSin(x+t)  )   
			//(  rCosxCost -rSinxSint   , rSinxCost + rCosxSint   )
			//as rCosx = x and rSinx = y
			// ( xCost- ySint , yCost + xSint  )

			//translate point, so that square is at center
			a_point -= a_squareCenter;

			//rotate point , so that square sides are parallel to the axis
			a_point = RotatePoint(a_point, -a_squareRotationAngle);

			m_rotatedPoint = a_point;

			if (a_point.x <= a_squareLength / 2 && a_point.x >= -a_squareLength / 2 &&
				a_point.y <= a_squareLength / 2 && a_point.y >= -a_squareLength / 2)
				return true;

			return false;
		}
		Vector2 RotatePoint(Vector2 a_point, float a_angle)
		{
			float l_cosT = Mathf.Cos(a_angle * Mathf.Deg2Rad);
			float l_sinT = Mathf.Sin(a_angle * Mathf.Deg2Rad);

			return new Vector2(
				a_point.x * l_cosT - a_point.y * l_sinT,
				a_point.x * l_sinT + a_point.y * l_cosT
				);
		}
		void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(m_Point,0.05f);
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(m_rotatedPoint, 0.05f);
		}
	
	}
}   
