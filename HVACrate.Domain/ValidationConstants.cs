namespace HVACrate.Domain
{
    public static class ValidationConstants
    {
        public static class ErrorMessages
        {
            public const string StringLength = "The {0} must be at least {2} and at max {1} characters long.";
        }

        public static class Project
        {
            /// <summary>
            /// The maximum length of the project's name.
            /// </summary>
            public const int NameMaxLength = 100;

            /// <summary>
            /// The minimum length of the project's name.
            /// </summary>
            public const int NameMinLength = 3;
        }

        public static class Building
        {
            /// <summary>
            /// The maximum length of the building's name.
            /// </summary>
            public const int NameMaxLength = 80;

            /// <summary>
            /// The minimum length of the building's name.
            /// </summary>
            public const int NameMinLength = 3;

            /// <summary>
            /// The maximum length of the building's location.
            /// </summary>
            public const int LocationMaxLength = 120;

            /// <summary>
            /// The minimum length of the building's location.
            /// </summary>
            public const int LocationMinLength = 3;
        }

        public static class Room
        {
            /// <summary>
            /// The maximum length of the room's type.
            /// </summary>
            public const int TypeMaxLength = 50;

            /// <summary>
            /// The minimum length of the room's type.
            /// </summary>
            public const int TypeMinLength = 2;

            /// <summary>
            /// The maximum length of the room's number.
            /// </summary>
            public const int NumberMaxLength = 10;

            /// <summary>
            /// The minimum length of the room's number.
            /// </summary>
            public const int NumberMinLength = 1;
        }

        public static class BuildingEnvelope
        {
            /// <summary>
            /// The default value of the adjusted temperature
            /// </summary>
            public const double AdjustedTemperatureDefaultValue = 0.0;
        }

        public static class Material
        {
            /// <summary>
            /// The maximum length of a material type string.
            /// </summary>
            public const int TypeMaxLength = 60;
        }

        public static class HVACUser
        {
            /// <summary>
            /// The maximum length of the user's first name.
            /// </summary>
            public const int FirstNameMaxLength = 80;

            /// <summary>
            /// The minimum length of the user's first name.
            /// </summary>
            public const int FirstNameMinLength = 2;

            /// <summary>
            /// The maximum length of the user's last name.
            /// </summary>
            public const int LastNameMaxLength = 100;

            /// <summary>
            /// The minimum length of the user's last name.
            /// </summary>
            public const int LastNameMinLength = 2;

            /// <summary>
            /// The maximum length of the user's username.
            /// </summary>
            public const int UserNameMaxLength = 30;

            /// <summary>
            /// The minimum length of the user's username.
            /// </summary>
            public const int UserNameMinLength = 3;
        }
    }
}
