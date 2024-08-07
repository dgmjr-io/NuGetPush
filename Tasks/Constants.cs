/*
 * Constants.cs
 *     Created: 2024-08-04T06:32:14-04:00
 *    Modified: 2024-08-04T06:32:14-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace NuGetPush.Tasks;

public static class Constants
{
    public const string DefaultDynamicTargetsFilePath = "NuGet.Dynamic.targets";

    public static class MetadataNames
    {
        public const string Name = nameof(Name);
        public const string ApiKey = nameof(ApiKey);
    }
}
