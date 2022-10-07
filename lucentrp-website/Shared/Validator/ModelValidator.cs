using FluentValidation;

namespace LucentRP.Shared.Validator
{
    /// <summary>
    /// An abstract validator that is used to validate objects that model database rows.
    /// </summary>
    /// <typeparam name="T">The type of object being validated.</typeparam>
    public abstract class ModelValidator<T> : AbstractValidator<T>
    {
        /// <summary>
        /// The configuration containing validation rules.
        /// </summary>
        private readonly IConfigurationRoot _config;

        /// <summary>
        /// The base path of the configuration section containing the validation rules.
        /// </summary>
        private readonly string _basePath;

        /// <summary>
        /// Construct a new ModelValidator.
        /// </summary>
        /// <param name="config">The configuration containing validation rules.</param>
        /// <param name="basePath">The base path of the configuration section containing the validation rules.</param>
        protected ModelValidator(IConfigurationRoot config, string basePath)
        {
            _config = config;
            _basePath = basePath;
        }

        /// <summary>
        /// Read a string from the config.
        /// </summary>
        /// <param name="subPath">The path of the configuration relative to the base path specified in the constructor.</param>
        /// <returns>The string value.</returns>
        public string GetString(string subPath) => _config[_basePath + subPath];

        /// <summary>
        /// Read a short from the config.
        /// </summary>
        /// <param name="subPath">The path of the configuration relative to the base path specified in the constructor.</param>
        /// <returns>The short value.</returns>
        public int GetShort(string subPath) => short.Parse(GetString(subPath));

        /// <summary>
        /// Read a int from the config.
        /// </summary>
        /// <param name="subPath">The path of the configuration relative to the base path specified in the constructor.</param>
        /// <returns>The int value.</returns>
        public int GetInt(string subPath) => int.Parse(GetString(subPath));

        /// <summary>
        /// Read a long from the config.
        /// </summary>
        /// <param name="subPath">The path of the configuration relative to the base path specified in the constructor.</param>
        /// <returns>The long value.</returns>
        public long GetLong(string subPath) => long.Parse(GetString(subPath));

        /// <summary>
        /// Read a bool from the config.
        /// </summary>
        /// <param name="subPath">The path of the configuration relative to the base path specified in the constructor.</param>
        /// <returns>The bool value.</returns>
        public bool GetBool(string subPath) => bool.Parse(GetString(subPath));
    }
}