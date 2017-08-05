﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HardAI: Player, AiListener {

	private GameBoardData board;

	private int playerMe;
	private int minDeep;

	private int countRatings;
	private List<int> validTurns;

	private String name;

	public HardAI (int playerMe, int deep) {
		this.playerMe = playerMe;
		countRatings = 0;
		this.minDeep = deep;

		name = "AI";
	}

	public HardAI(int playerMe, int deep, String name) {
		this.playerMe = playerMe;
		countRatings = 0;
		this.minDeep = deep;

		this.name = name;
	}

	public void calcNextMove (int player, GameBoardData gameBoard) {
		countRatings = 0;

		board = gameBoard;
		validTurns = board.getValidTurns ();

		turnHighestRating = validTurns [0];
		highestRating = int.MinValue;

		for (int i = 0; i < validTurns.Count; i++) {
			DeepSearch deepSearch = new DeepSearch (board, validTurns[i], minDeep, playerMe, playerMe, int.MinValue, int.MaxValue);
			deepSearch.setAiListener (this);
			deepSearch.Start ();
		}
	}

	//Choose the highest rating
	int highestRating;
	int turnHighestRating;

	public void calculatedRating(int turn, int rating) {
		countRatings++;

		//Debug.Log ("Rating: " + rating + ", validTurn: " + turn);
		if (rating > highestRating) {
			highestRating = rating;
			turnHighestRating = turn;
		}
	}

	public bool finishedCalc() {
		if (validTurns == null) {
			return false;
		}
		return countRatings == validTurns.Count;
	}

	public int getMove() {
		return turnHighestRating;
	}

	public String getName() {
		return name;
	}
}

