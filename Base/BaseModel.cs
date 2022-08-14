namespace MVCSandBox.Base
{
    public class BaseModel
    {
        public BaseModel(Guid id)
        {
            Id = id;
        }

        public BaseModel()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Esta sobrecarga cuidará de validar para qualquer entidade do tipo BaseModel se elas são iguais.
        /// Poderá ser usada para idas desnecessárias ao BD.
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj == null)
                throw new InvalidOperationException("The type is not suported.");

            if (!obj.GetType().IsInstanceOfType(this))
                throw new InvalidOperationException("The type is not suported.");

            Type thisType = GetType();
            Type objType = obj.GetType();

            System.Reflection.PropertyInfo[] thisProperties = thisType.GetProperties();
            System.Reflection.PropertyInfo[] objProperties = objType.GetProperties();

            for (int i = 0; i < thisProperties.Length; i++)
            {
                if (thisProperties[i].GetValue(this) != objProperties[i].GetValue(obj))
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public Guid Id { get; private set; }
    }
}
