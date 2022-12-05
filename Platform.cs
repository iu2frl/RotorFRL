/*
 *
 *
 *    Platform.cs
 *
 *    Stuff that is platform/runtime dependent.
 *
 *    License: GNU General Public License Version 3.0.
 *    
 *    Copyright (C) 2014-2015 by Matthew K. Roberts, KK5JY. All rights reserved.
 *
 *    This program is free software: you can redistribute it and/or modify
 *    it under the terms of the GNU General Public License as published by
 *    the Free Software Foundation, either version 3 of the License, or
 *    (at your option) any later version.
 *    
 *    This program is distributed in the hope that it will be useful, but
 *    WITHOUT ANY WARRANTY; without even the implied warranty of
 *    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 *    General Public License for more details.
 *    
 *    You should have received a copy of the GNU General Public License
 *    along with this program.  If not, see: http://www.gnu.org/licenses/
 *    
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KK5JY.RotorCraft {
	/// <summary>
	/// Platform and release-specific fields.
	/// </summary>
	public static class Platform {
		public const string ProductName = "RotorCraft";
		public const string VersionNumberString = "1.0";
		public const string ReleaseNumberString = "20150912a";
		public static readonly string VersionString;

		static Platform() {
			VersionString = ProductName + " "
#if DEBUG
				+ "DEBUG "
#endif
				+ VersionNumberString
				+ "-"
				+ ReleaseNumberString;
		}

		/// <summary>
		/// The path to user-specific profiles
		/// </summary>
		public static readonly string UserProfileFolder
			= System.IO.Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				ProductName,
				"Profiles");

		/// <summary>
		/// The path to the standard configuration location.
		/// </summary>
		public static string ConfigFilePath {
			get {
				var folder = Environment.SpecialFolder.CommonApplicationData;
				if (Environment.OSVersion.Platform == PlatformID.Unix) {
					folder = Environment.SpecialFolder.ApplicationData;
				}
				return System.IO.Path.Combine(
					Environment.GetFolderPath(folder),
					ProductName,
					"Config.xml");
			}
		}

		/// <summary>
		/// Creates the config file paths if they don't exist.
		/// </summary>
		public static bool CreateProfileFolders() {
			try {
				System.IO.Directory.CreateDirectory(UserProfileFolder);
				System.IO.Directory.CreateDirectory(
					System.IO.Path.GetDirectoryName(ConfigFilePath)
				);
				return true;
			} catch {
				return false;
			}
		}

	}
}
