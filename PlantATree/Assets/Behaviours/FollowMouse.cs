using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Interactivity;

// This Action will move the element according to Mouse movement.

namespace GamePack
{
    public class FollowMouse : TriggerAction<DependencyObject>
    {
		public enum PositionOptions { None, X, Y, XY };

		// Private Properties
		private double maxX, maxY;
		private double minX, minY;

        private double width = 0;
        private double height = 0;
        private double oX = 0;
        private double oY = 0;

		private bool IsMouseFirstOver = false;

		// Vectors for Position and Velocity
		Vector mousePosition = new Vector();
		Vector targetPosition = new Vector();
		double targetRotation = 0;
		double angularSpeed = 0;
		Vector velocity = new Vector(0, 0);

		private FrameworkElement target;
		private UIElement _parent;

        #region Property to Expose

        [Category("Mouse Properties")]
		public PositionOptions FollowPosition { get { return (PositionOptions)GetValue(FollowPositionProperty); } set { SetValue(FollowPositionProperty, value); } }
		[Category("Mouse Properties")]
		public bool FollowRotation { get { return (bool)GetValue(FollowRotationProperty); } set { SetValue(FollowRotationProperty, value); } }
		[Category("Mouse Properties")]
		public Thickness Margin { get { return (Thickness)GetValue(MarginProperty); } set { SetValue(MarginProperty, value); } }
		[Category("Mouse Properties")]
		public double Easing { get { return (double)GetValue(EasingProperty); } set { SetValue(EasingProperty, value); } }

		public static readonly DependencyProperty FollowPositionProperty = DependencyProperty.Register("FollowPosition", typeof(PositionOptions), typeof(FollowMouse), null);
		public static readonly DependencyProperty FollowRotationProperty = DependencyProperty.Register("FollowRotation", typeof(bool), typeof(FollowMouse), null);
		public static readonly DependencyProperty MarginProperty = DependencyProperty.Register("MarginRotation", typeof(Thickness), typeof(FollowMouse), null);
		public static readonly DependencyProperty EasingProperty = DependencyProperty.Register("EasingRotation", typeof(double), typeof(FollowMouse), null);

        #endregion


        protected override void Invoke(object o)
        {
			target = this.AssociatedObject as FrameworkElement;
			_parent = VisualTreeHelper.GetParent(target) as UIElement;

			// Calculate Max/Min
			FrameworkElement container = _parent as FrameworkElement;
			maxX = container.Width - target.Width - Margin.Right;
			minX = 0 + Margin.Left;
			maxY = container.Height - target.Height - Margin.Bottom;
			minY = 0 + Margin.Top;

            oX = target.RenderTransformOrigin.X;
            oY = target.RenderTransformOrigin.Y;
            width = target.Width;
            height = target.Height;

            targetPosition.X = Canvas.GetLeft(target) + (width * oX);
            targetPosition.Y = Canvas.GetTop(target) + (height * oX);

			container.MouseMove += new MouseEventHandler(HandleMouseMove);

			if (Easing > 0) CompositionTarget.Rendering += new EventHandler(EasingRendering);
        }

		void EasingRendering(object sender, EventArgs e)
		{
			if (IsMouseFirstOver)
			{
				if (FollowPosition != PositionOptions.None)
				{
					switch (FollowPosition)
					{
						case PositionOptions.X:
							velocity.X = (mousePosition.X - targetPosition.X) * Easing;
							break;
						case PositionOptions.Y:
							velocity.Y = (mousePosition.Y - targetPosition.Y) * Easing;
							break;
						case PositionOptions.XY:
							velocity.X = (mousePosition.X - targetPosition.X) * Easing;
							velocity.Y = (mousePosition.Y - targetPosition.Y) * Easing;
							break;
					}
				}
				else
				{
					velocity.X = 0;
					velocity.Y = 0;
				}
				targetPosition += velocity;
				if ((targetPosition.X - (width * oX)) > maxX) targetPosition.X = maxX + (width * oX);
				if ((targetPosition.X - (width * oX)) < minX) targetPosition.X = minX + (width * oX);
				if ((targetPosition.Y - (height * oY)) > maxY) targetPosition.Y = maxY + (height * oY);
				if ((targetPosition.Y - (height * oY)) < minY) targetPosition.Y = minY + (height * oY);
				Position(targetPosition);

				if (FollowRotation)
				{
					RotateTransform rotation = new RotateTransform();
					double centerX = (targetPosition.X);
					double centerY = (targetPosition.Y);
					double dPhi = ((Math.Atan2(mousePosition.Y - centerY, mousePosition.X - centerX) * 180 / Math.PI) - targetRotation);
					angularSpeed = dPhi * Easing;
					if (Math.Abs(dPhi) > 180)
					{
						targetRotation = (Math.Atan2(mousePosition.Y - centerY, mousePosition.X - centerX) * 180 / Math.PI);
						rotation.Angle = targetRotation;
						target.RenderTransform = rotation;
					}
					else
					{
						targetRotation += angularSpeed;
						rotation.Angle = targetRotation;
						target.RenderTransform = rotation;
					}
				}

			}
		}

        private void Position(Vector p)
        {
            Canvas.SetLeft(target, p.X - (width * oX));
            Canvas.SetTop(target, p.Y - (height * oY));
        }

		void HandleMouseMove(object sender, MouseEventArgs e)
		{
			UIElement reference = sender as UIElement;
			IsMouseFirstOver = true;
			Point mouse = e.GetPosition(reference);

			double newX = mouse.X - (width * oX);
			double newY = mouse.Y - (height * oY);
			if (newX <= minX) newX = minX;
			if (newX >= maxX) newX = maxX;
			if (newY <= minY) newY = minY;
			if (newY >= maxY) newY = maxY;

			if (Easing > 0)
			{
				if (FollowPosition != PositionOptions.None)
				{
					switch (FollowPosition)
					{
						case PositionOptions.X:
							mousePosition.X = mouse.X;
							break;
						case PositionOptions.Y:
							mousePosition.Y = mouse.Y;
							break;
						case PositionOptions.XY:
							mousePosition.X = mouse.X;
							mousePosition.Y = mouse.Y;
							break;
					}
				}
				if (FollowRotation)
				{
					mousePosition.X = mouse.X;
					mousePosition.Y = mouse.Y;
				}
			}
			else
			{
				if (FollowPosition != PositionOptions.None)
				{
					switch (FollowPosition)
					{
						case PositionOptions.X:
							Canvas.SetLeft(target, newX);
							break;
						case PositionOptions.Y:
							Canvas.SetTop(target, newY);
							break;
						case PositionOptions.XY:
							Canvas.SetLeft(target, newX);
							Canvas.SetTop(target, newY);
							break;
					}
				}
				if (FollowRotation)
				{
					RotateTransform rotation = new RotateTransform();
					double centerY = (Canvas.GetTop(target) + (height * oY));
					double centerX = (Canvas.GetLeft(target) + (width * oX));
					rotation.Angle = (Math.Atan2(mouse.Y - centerY, mouse.X - centerX) * 180 / Math.PI);

					target.RenderTransform = rotation;
				}
			}
            

		}

    }
}
