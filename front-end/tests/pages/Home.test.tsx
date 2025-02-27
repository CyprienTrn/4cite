import { render, screen } from "@testing-library/react";
import "@testing-library/jest-dom";
import Home from "../../src/pages/Home";
import React from "react";

test("affiche un bouton 'Se connecter'", () => {
  render(<Home />);
  const loginButton = screen.getByRole("button", { name: /se connecter/i });
  expect(loginButton).toBeInTheDocument();
});

test("affiche un bouton 'S'inscrire'", () => {
  render(<Home />);
  const signupButton = screen.getByRole("button", { name: /s'inscrire/i });
  expect(signupButton).toBeInTheDocument();
});
