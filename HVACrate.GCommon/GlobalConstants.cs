namespace HVACrate.GCommon
{
    public static class GlobalConstants
    {
        /// <summary>
        /// The default format for dates in the application.
        /// </summary>
        public const string DateFormat = "yyyy-MM-dd";

        /// <summary>
        /// The maximum length of the building's image URL.
        /// </summary>
        public const int ImageUrlMaxLength = 2048;

        /// <summary>
        /// The default url for users without profile picture.
        /// </summary>
        public const string DefaultProfilePictureUrl = "/images/profile-pictures/default.jpg";

        /// <summary>
        /// The claim key for profile picture.
        /// </summary>
        public const string ProfilePictureClaimType = "HVACProfilePicture";

        /// <summary>
        /// The maximum precision for decimal values of the models.
        /// </summary>
        public const int TotalPrecision = 18;

        /// <summary>
        /// The maximum scale for decimal values of the models.
        /// </summary>
        public const int TotalScale = 2;
    }
}
