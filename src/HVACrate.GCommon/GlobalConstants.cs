namespace HVACrate.GCommon
{
    public static class GlobalConstants
    {
        /// <summary>
        /// The default format for dates in the application.
        /// </summary>
        public const string DateFormat = "yyyy-MM-dd";

        /// <summary>
        /// The date format that includes hours and minutes.
        /// </summary>
        public const string DescriptiveDateFormat = "yyyy-MM-dd HH:mm";

        /// <summary>
        /// The maximum length of the building's image URL.
        /// </summary>
        public const int ImageUrlMaxLength = 2048;

        /// <summary>
        /// The default url for users without profile picture.
        /// </summary>
        public const string DefaultProfilePictureUrl = "/images/profile-pictures/default.jpg";

        /// <summary>
        /// The default image url for buildings without uploaded picture.
        /// </summary>
        public const string DefaultBuildingImageUrl = "/images/buildings/default_building.jpg";

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

        /// <summary>
        /// The properties which are used for querying for the models
        /// </summary>
        public static class QueryProperties
        {
            public const string UserQueryParam = "UserName";
            public const string ProjectQueryParam = "Name";
            public const string BuildingQueryParam = "Name";
            public const string RoomQueryParam = "Floor";
        }
    }
}
