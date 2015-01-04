﻿namespace TuringSimulator.CS.Core
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using System.Text;
	using Shared;

	public class TuringLogic : ITuringLogic
	{
		private int position;

		private int currentState; // MZ - Maschinenzustand

		private MovementValues nextMove;

		public ITuringCommandList CommandList { get; private set; }

		public char[] Tape { get; private set; }

		public int Position
		{
			get { return this.position; }
		}

		public MovementValues NextMove
		{
			get { return this.nextMove; }
		}

		public char CurrentTapeChar
		{
			get { return this.Tape[this.Position]; }	
		}

		public bool Terminated { get; private set; }

		public void Initialize(ITuringCommandList turingCommandList, string inputString)
		{
			this.CommandList = turingCommandList;
			this.Tape = inputString.ToCharArray();
			this.position = 0;
			this.Terminated = false;
			this.nextMove = MovementValues.Undefined;
		}

		public void Start()
		{
			if ( this.Tape == null )
				throw new NullReferenceException("Turing-Logik nicht initialisiert! Bitte zuerst Initialize() aufrufen.");

			while ( this.Step() )
			{
			}
		}

		public bool Step()
		{
			Debug.WriteLine(string.Format("Pre-Step : Pos: {0} | char: {1} | nMov: {2} | Tape: {3}", this.position, this.CurrentTapeChar, this.nextMove, this.Tape.Length < 50 ? new string(this.Tape) : "Tape zu lang (> 50)"));
			
			switch ( this.nextMove )
			{
				case MovementValues.S:
					this.Terminated = true;
					return false;
				case MovementValues.R:
					this.position++;
					break;
				case MovementValues.L:
					this.position--;
					break;
				case MovementValues.Undefined:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			var tempChar = this.CurrentTapeChar;
			var command = this.CommandList.SelectCommand(this.currentState, tempChar);
			this.currentState = command.Z1;
			
			// Zeichen auf Tape ggfls. ersetzen lt. Anweisung
			if ( command.SZ != '#' )
				this.Tape[this.position] = command.SZ;

			this.nextMove = command.MOV;
			Debug.WriteLine(string.Format("Post-Step: Pos: {0} | char: {1} -> {2} | Mov: {3} | Tape: {4}", this.position, tempChar, this.CurrentTapeChar, command.MOV, this.Tape.Length < 50 ? new string(this.Tape) : "Tape zu lang (> 50)"));
			return true;
		}

		public void Reset()
		{
			this.position = 0;
			this.Terminated = false;
			this.Tape = null;
			this.nextMove = MovementValues.Undefined;
		}
	}
}