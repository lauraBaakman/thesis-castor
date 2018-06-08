using UnityEngine;
using System;
using Utils;

namespace Registration
{
	public class Point : IEquatable<Point>, IComparable<Point>
	{
		private static Color DefaultColor = Color.white;
		private static readonly Vector3 NoNormal = new Vector3();
		private static ColorGenerator colorGenerator = new ColorGenerator();

		#region position
		/// <summary>
		/// Gets the position of the point.
		/// </summary>
		/// <value>The position.</value>
		public Vector3 Position
		{
			get { return position; }
		}
		private Vector3 position;
		#endregion

		#region color
		/// <summary>
		/// Gets or sets the color of the point.
		/// </summary>
		/// <value>The color.</value>
		public Color Color
		{
			get { return color; }
			set { color = value; }
		}
		private Color color;
		#endregion

		#region normal
		/// <summary>
		/// Gets the normal of the point. If the point has no normal an error 
		/// is thrown.
		/// </summary>
		/// <value>The normal.</value>
		public Vector3 Normal
		{
			get
			{
				if (hasNormal()) return normal;
				throw new Exception("The normal of this Point has not been set.");
			}
		}
		private Vector3 normal;

		/// <summary>
		/// Gets the unit normal of the point. If the point has no normal an 
		/// error is thrown.
		/// </summary>
		/// <value>The unit normal.</value>
		public Vector3 UnitNormal
		{
			get
			{
				return Normal.normalized;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this 
		/// <see cref="T:Registration.Point"/> has normal.
		/// </summary>
		/// <value><c>true</c> if has normal; otherwise, <c>false</c>.</value>
		public bool HasNormal
		{
			get { return hasNormal(); }
		}
		#endregion

		public Point(Vector3 position, Vector3 normal)
		{
			this.position = position;
			this.Color = colorGenerator.GetNextColor();
			this.normal = normal;
		}

		public Point(Vector3 position)
			: this(position, normal: NoNormal)
		{ }

		private Point(Vector3 position, Vector3 normal, Color color)
		{
			this.position = position;
			this.normal = normal;
			this.color = color;
		}

		private bool hasNormal()
		{
			return this.normal != NoNormal;
		}

		/// <summary>
		/// Resets the color of the point to the default color.
		/// </summary>
		public void ResetColor()
		{
			this.Color = DefaultColor;
		}

		/// <summary>
		///Compares this point to anouther point based on their position. If 
		/// the points have the same position their normals are compared.
		/// </summary>
		/// <returns>The to.</returns>
		/// <param name="other">Other.</param>
		public int CompareTo(Point other)
		{
			if (other == null) return 1;

			int positionComparison = this.position.CompareTo(other.position);
			if (positionComparison != 0) return positionComparison;

			return this.normal.CompareTo(other.normal);
		}

		/// <summary>
		/// Determines whether the specified <see cref="object"/> is equal to 
		/// the current <see cref="T:Registration.Point"/>. Based on their 
		/// position and normal.
		/// </summary>
		/// <param name="obj">The <see cref="object"/> to compare with the 
		/// current <see cref="T:Registration.Point"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="object"/> is equal 
		/// to the current
		/// <see cref="T:Registration.Point"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
				return false;
			return this.Equals(obj as Point);
		}

		/// <summary>
		/// Determines whether the specified <see cref="Registration.Point"/> is 
		/// equal to the current <see cref="T:Registration.Point"/>. Based on its 
		/// position and normal.
		/// </summary>
		/// <param name="other">The <see cref="Registration.Point"/> to compare 
		/// with the current <see cref="T:Registration.Point"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="Registration.Point"/> 
		/// is equal to the current
		/// <see cref="T:Registration.Point"/>; otherwise, <c>false</c>.</returns>
		public bool Equals(Point other)
		{
			if (other == null) return false;

			bool positionEqual = (this.Position == other.Position);
			bool normalEqual = (this.normal == other.normal);

			return (positionEqual && normalEqual);
		}

		/// <summary>
		/// Serves as a hash function for a <see cref="T:Registration.Point"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in 
		/// hashing algorithms and data structures such as a
		/// hash table.</returns>
		public override int GetHashCode()
		{
			int hashCode = 67;

			hashCode = hashCode * 71 + position.GetHashCode();
			if (hasNormal()) hashCode = hashCode * 71 + normal.GetHashCode();

			return hashCode;
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current 
		/// <see cref="T:Registration.Point"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current 
		/// <see cref="T:Registration.Point"/>.</returns>
		public override string ToString()
		{
			if (hasNormal())
			{
				return string.Format(
					 "[Point: Position={0}, Normal={1}]", Position, Normal
				 );
			}
			return string.Format("[Point: Position={0}, no normal]", Position);
		}

		/// <summary>
		/// Convert the point to a ray in world space shooting out of the model.
		/// </summary>
		/// <returns>The ray starting at thhe position of this point in the direction of its normal.</returns>
		/// <param name="localTransform">The local transform of the point.</param>
		/// <param name="epsilon">Offset of the origin of the ray w.r.t. to the point in the direction of the normal.</param>
		public Ray ToForwardWorldSpaceRay(Transform localTransform, float epsilon)
		{
			return ToWorldSpaceRay(localTransform, +1, epsilon);
		}

		/// <summary>
		/// Convert the point to a ray in world space shooting inside of the model
		/// </summary>
		/// <returns>The ray starting at thhe position of this point in the direction of its normal.</returns>
		/// <param name="localTransform">The local transform of the point.</param>
		/// <param name="epsilon">Offset of the origin of the ray w.r.t. to the point in the direction of the normal.</param>
		public Ray ToBackwardWorldSpaceRay(Transform localTransform, float epsilon)
		{
			return ToWorldSpaceRay(localTransform, -1, epsilon);
		}

		/// <summary>
		/// Compute a world space ray based on the point, that points either out of the model or inside it.
		/// </summary>
		/// <returns>The world space ray.</returns>
		/// <param name="localTransform">Local transform.</param>
		/// <param name="direction">Direction -1 means the ray points inside the model, direction is one means the ray points outside the model..</param>
		/// <param name="epsilon">Offset of the origin of the ray</param>
		private Ray ToWorldSpaceRay(Transform localTransform, int direction, float epsilon)
		{
			Vector3 worldSpacePosition = localTransform.TransformPoint(Position - epsilon * direction * Normal.normalized);
			Vector3 worldSpaceDirection = localTransform.TransformDirection(direction * Normal);

			worldSpaceDirection.Normalize();

			return new Ray(
				origin: worldSpacePosition,
				direction: worldSpaceDirection
			);
		}

		/// <summary>
		/// Returns the position of this point as a homogeneous vector.
		/// </summary>
		/// <returns>The homogeneous vector4.</returns>
		public Vector4 ToHomogeneousVector4()
		{
			return new Vector4(Position.x, Position.y, Position.z, 1.0f);
		}

		/// <summary>
		/// Transform this point from sourceTransform to destinationTransform.
		/// </summary>
		/// <returns>The other transform.</returns>
		/// <param name="sourceTransform">The current transform of the point.</param>
		/// <param name="destinationTransform">The destination transform.</param>
		public Point ChangeTransform(Transform sourceTransform, Transform destinationTransform)
		{
			return new Point(
				position: Position.ChangeTransformOfPosition(sourceTransform, destinationTransform),
				normal: HasNormal ? Normal.ChangeTransformOfDirection(sourceTransform, destinationTransform) : NoNormal
			);
		}

		/// <summary>
		/// Transform this points with the passed transformation matrix. Does not 
		/// changes the normal.
		/// </summary>
		/// <returns>The transform.</returns>
		/// <param name="transformation">Transformation.</param>
		public Point ChangeTransform(Matrix4x4 transformation)
		{
			return new Point(
				position: transformation.MultiplyPoint3x4(Position),
				normal: HasNormal ? transformation.MultiplyVector(Normal) : NoNormal
			);
		}

		/// <summary>
		/// Applies the transformation matrix to this point. Changes the normal.
		/// </summary>
		/// <returns>The transform.</returns>
		/// <param name="transformationMatrix">Transformation matrix.</param>
		public Point ApplyTransform(Matrix4x4 transformationMatrix)
		{
			Vector3 transformedPosition = transformationMatrix.MultiplyPoint(this.Position);
			return new Point(transformedPosition, this.normal, this.Color);
		}
	}
}