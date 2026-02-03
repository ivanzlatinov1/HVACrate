namespace HVACrate.Domain
{
    public static class ValidationConstants
    {
        public record ValidationMessages
        {
            /// <summary>
            /// Error message shown when a required field is missing.
            /// The placeholder {0} is replaced with the field name.
            /// </summary>
            public const string Required = "The {0} field is required.";

            /// <summary>
            /// Error message shown when a string length is out of allowed range.
            /// Placeholders: {0} = field name, {1} = max length, {2} = min length.
            /// </summary>
            public const string StringLength = "The {0} must be at least {2} and at max {1} characters long.";

            /// <summary>
            /// Error message shown when an invalid email format is entered.
            /// The placeholder {0} is replaced with the field name.
            /// </summary>
            public const string InvalidEmail = "The {0} field is not a valid email address.";

            /// <summary>
            /// Error message shown when an invalid URL format is entered.
            /// The placeholder {0} is replaced with the field name.
            /// </summary>
            public const string InvalidUrl = "The {0} field must be a valid URL.";

            /// <summary>
            /// Error message shown when a temperature value is outside the allowed range.
            /// </summary>
            public const string TemperatureRange = "Temperature must be between -100 and 100.";

            /// <summary>
            /// Building Envelope validation messages.
            /// </summary>
            public record BuildingEnvelopeValidationMessages
            {
                public const string HeightRange = "Height must be between {1} and {2} meters.";
                public const string WidthRange = "Width must be between {1} and {2} meters.";
                public const string DensityRange = "Density must be between {1} and {2} kg/m³.";
                public const string HeatTransferCoefficientRange = "Heat transfer coefficient must be between {1} and {2} W/m²·K.";
                public const string MaterialRequired = "Material is required!";
            }
        }

        public record Project
        {
            /// <summary>
            /// Maximum allowed length for the project name to ensure consistency and avoid database overflow.
            /// </summary>
            public const int NameMaxLength = 100;

            /// <summary>
            /// Minimum allowed length for the project name to avoid overly short or meaningless names.
            /// </summary>
            public const int NameMinLength = 3;

            /// <summary>
            /// Minimum valid temperature value allowed for project region settings, representing extreme cold.
            /// </summary>
            public const int MinRegionTemperature = -100;

            /// <summary>
            /// Maximum valid temperature value allowed for project region settings, representing extreme heat.
            /// </summary>
            public const int MaxRegionTemperature = 100;
        }

        public record Building
        {
            /// <summary>
            /// Maximum allowed length for the building's name to maintain readability and storage limits.
            /// </summary>
            public const int NameMaxLength = 80;

            /// <summary>
            /// Minimum allowed length for the building's name to prevent empty or too short names.
            /// </summary>
            public const int NameMinLength = 3;

            /// <summary>
            /// Maximum length allowed for building location description, supporting detailed addresses.
            /// </summary>
            public const int LocationMaxLength = 120;

            /// <summary>
            /// Minimum length required for building location to ensure meaningful data entry.
            /// </summary>
            public const int LocationMinLength = 3;
        }

        public record Room
        {
            /// <summary>
            /// Maximum allowed length for room type names to ensure clarity and consistency.
            /// </summary>
            public const int TypeMaxLength = 50;

            /// <summary>
            /// Minimum length for room type names to prevent overly brief or invalid entries.
            /// </summary>
            public const int TypeMinLength = 2;

            /// <summary>
            /// Maximum allowed length for room number strings, accommodating complex numbering schemes.
            /// </summary>
            public const int NumberMaxLength = 10;

            /// <summary>
            /// Minimum length for room numbers, allowing at least a single character.
            /// </summary>
            public const int NumberMinLength = 1;

            /// <summary>
            /// Minimum allowed temperature value for rooms, covering extreme cold scenarios.
            /// </summary>
            public const int MinTemperature = -100;

            /// <summary>
            /// Maximum allowed temperature value for rooms, covering extreme heat scenarios.
            /// </summary>
            public const int MaxTemperature = 100;
        }

        public record BuildingEnvelope
        {
            /// <summary>
            /// Default adjusted temperature value used when no specific temperature is provided.
            /// </summary>
            public const double AdjustedTemperatureDefaultValue = 0.0;

            /// <summary>
            /// Minimum valid height of the building envelope, in meters.
            /// Values below this indicate invalid or non-physical dimensions.
            /// </summary>
            public const double MinHeight = 0.1;

            /// <summary>
            /// Maximum valid height of the building envelope, in meters.
            /// Values above this may indicate data entry errors or unrealistic measurements.
            /// </summary>
            public const double MaxHeight = 1000;

            /// <summary>
            /// Minimum valid width of the building envelope, in meters.
            /// Used to validate physical plausibility of building dimensions.
            /// </summary>
            public const double MinWidth = 0.1;

            /// <summary>
            /// Maximum valid width of the building envelope, in meters.
            /// Values exceeding this are considered invalid or unrealistic.
            /// </summary>
            public const double MaxWidth = 1000;

            /// <summary>
            /// Minimum density value for materials used in the building envelope, in kg/m³.
            /// Ensures material densities stay within physically meaningful ranges.
            /// </summary>
            public const double MinDensity = 0.01;

            /// <summary>
            /// Maximum density value for materials in the building envelope, in kg/m³.
            /// Values above this indicate potential data errors or incorrect units.
            /// </summary>
            public const double MaxDensity = 10000;

            /// <summary>
            /// Minimum allowed temperature value for building envelopes, covering extreme cold scenarios.
            /// </summary>
            public const int MinTemperature = -100;

            /// <summary>
            /// Maximum allowed temperature value for building envelopes, covering extreme heat scenarios.
            /// </summary>
            public const int MaxTemperature = 100;

            /// <summary>
            /// Minimum heat transfer coefficient for building materials, in W/m²·K.
            /// Ensures thermal properties are within reasonable physical limits.
            /// </summary>
            public const double MinHeatTransferCoefficient = 0.01;

            /// <summary>
            /// Maximum heat transfer coefficient for building materials, in W/m²·K.
            /// Values above this may be physically unrealistic or indicate errors.
            /// </summary>
            public const double MaxHeatTransferCoefficient = 100;
        }

        public record Material
        {
            /// <summary>
            /// Maximum length allowed for a material type name, ensuring database compatibility and readability.
            /// </summary>
            public const int TypeMaxLength = 60;

            /// <summary>
            /// Minimum length required for material type names to avoid empty or meaningless entries.
            /// </summary>
            public const int TypeMinLength = 3;
        }

        public record HVACUser
        {
            /// <summary>
            /// Maximum length for a user's first name, accommodating long names while maintaining UI consistency.
            /// </summary>
            public const int FirstNameMaxLength = 80;

            /// <summary>
            /// Minimum length for a user's first name to prevent invalid or placeholder data.
            /// </summary>
            public const int FirstNameMinLength = 2;

            /// <summary>
            /// Maximum length for a user's last name, ensuring proper storage and display.
            /// </summary>
            public const int LastNameMaxLength = 100;

            /// <summary>
            /// Minimum length for a user's last name to avoid invalid entries.
            /// </summary>
            public const int LastNameMinLength = 2;

            /// <summary>
            /// Maximum allowed length for usernames, balancing usability and system constraints.
            /// </summary>
            public const int UserNameMaxLength = 30;

            /// <summary>
            /// Minimum length for usernames to ensure they are meaningful and unique.
            /// </summary>
            public const int UserNameMinLength = 3;
        }
    }
}
