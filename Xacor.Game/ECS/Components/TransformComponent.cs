using System.Collections.Generic;
using Xacor.Math;

namespace Xacor.Game.ECS.Components
{
    public class TransformComponent : IComponent
    {
        private Vector3 _positionLocal;
        private Quaternion _rotationLocal;
        private Vector3 _scaleLocal;

        private Matrix _matrix;
        private Matrix _matrixLocal;
        private Vector3 _lookAt;

        private TransformComponent _parent;
        private List<TransformComponent> _children;

        public List<TransformComponent> Children => _children;
        public Matrix Matrix => _matrix;
        public Matrix MatrixLocal => _matrixLocal;

        public TransformComponent Parent
        {
            get => _parent;
            set
            {

            }
        }

        public Vector3 Position
        {
            get => _matrix.GetTranslation();
            set
            {
                if (EqualityComparer<Vector3>.Default.Equals(_positionLocal, value))
                {
                    return;
                }

                if (!HasParent())
                {
                    _positionLocal = value;
                }
                else
                {
                    var parentMatrix = Matrix.Invert(Parent.Matrix);
                    _positionLocal = parentMatrix.Transform(value);
                }
            }
        }

        public Vector3 PositionLocal
        {
            get => _positionLocal;
            set
            {
                _positionLocal = value;
                UpdateTransform();
            }
        }

        public TransformComponent Root
        {
            get;
            set;
        }

        public bool IsRoot()
        {
            return !HasParent();
        }

        public bool HasParent()
        {
            return _parent != null;
        }

        public TransformComponent()
        {
            _positionLocal = Vector3.Zero;
            _rotationLocal = Quaternion.Identity;
            _scaleLocal = Vector3.One;
            _matrix = Matrix.Identity;
            _matrixLocal = Matrix.Identity;
            _parent = null;
        }

        private Matrix GetParentTransformMatrix()
        {
            return HasParent() ? Parent.Matrix : Matrix.Identity;
        }

        private void UpdateTransform()
        {
            _matrixLocal = Matrix.CreateScale(_scaleLocal) *
                           Matrix.CreateRotation(_rotationLocal) *
                           Matrix.CreateTranslation(_positionLocal);
            if (!HasParent())
            {
                _matrix = _matrixLocal;
            }
            else
            {
                _matrix = _matrixLocal * GetParentTransformMatrix();
            }

            foreach (var child in _children)
            {
                child.UpdateTransform();
            }
        }
    }
}