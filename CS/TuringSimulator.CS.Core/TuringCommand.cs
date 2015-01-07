﻿// <copyright file="TuringCommand.cs" person="Sascha Schwegelbauer" company="Private">
//   Copyright (c) Sascha Schwegelbauer. All rights reserved.
// </copyright>

namespace TuringSimulator.CS.Core
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.Text;
	using System.Text.RegularExpressions;
	using TuringSimulator.CPP.Shared;

	/// <summary>
	/// The turing command.
	/// </summary>
	public class TuringCommand : ITuringCommand
	{
		public TuringCommand(string rawLine)
		{
			var regEx = Regex.Match(rawLine, Generic.CommandParseRegex, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
			if ( !regEx.Success )
				throw new FormatException("Kommandozeichenfolge konnte nicht gelesen werden.");

			this.Z0 = int.Parse(regEx.Groups["Z0"].Value);
			this.GZ = Convert.ToChar(regEx.Groups["GZ"].Value);
			this.Z1 = int.Parse(regEx.Groups["Z1"].Value);
			this.SZ = Convert.ToChar(regEx.Groups["SZ"].Value);
			this.MOV = (MovementValues)Enum.Parse(typeof(MovementValues), regEx.Groups["MOV"].Value);
		}

		public int Z0 { get; set; }

		public char GZ { get; set; }

		public int Z1 { get; set; }

		public char SZ { get; set; }

		public MovementValues MOV { get; set; }

		public int Line { get; set; }
	}
}