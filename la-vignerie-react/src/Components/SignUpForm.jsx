import React from "react";
import { useForm } from "react-hook-form";
import { Button, TextField, Typography, Container, Grid } from "@mui/material";

import {Link, useNavigate} from "react-router-dom"
import axios, {HttpStatusCode} from 'axios';
import {useCookies} from "react-cookie";

function SignUpForm() {
  const { register, handleSubmit, formState: { errors } } = useForm();
  
  const validateEmail = (value) => {
    // Utilisation d'une expression régulière pour valider le format de l'email
    const isValidEmail = /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value);
    if (!isValidEmail) {
      return "Adresse e-mail invalide";
    }
    return true;
  };

  const validatePassword = (value) => {
    const passwordRegex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()]).{8,}$/;
    if (!passwordRegex.test(value)) {
      return "Le mot de passe doit contenir au moins 8 caractères, une lettre majuscule, une lettre minuscule, un chiffre et un caractère spécial parmi !@#$%^&*()";
    }
    return true;
  };

  const validateConfirmPassword = (value) => {
    const password = watch("password");
    if (value !== password) {
      return "Les mots de passe ne correspondent pas";
    }
    return true;
  };

  const navigate = useNavigate();
  const onSubmit = async (data) => {
    // Envoyer les données du formulaire au serveur ou effectuer d'autres actions
    try {
      const response = await axios.post(import.meta.env.VITE_API_BASE_URL + '/User', {
        mail: data.email,
        password: data.password
      });
      navigate('login')

    } catch (error) {

        console.error('Une erreur s\'est produite lors de l\'envoi de la requête.', error);
      }
  };

  

  return (  
    <Container maxWidth="sm">
      <Typography variant="h4" gutterBottom>
        Créer un compte
      </Typography>
      <form onSubmit={handleSubmit(onSubmit)}>
        <Grid container spacing={2}>
          
          <Grid item xs={12}>
            <TextField
              fullWidth
              label="Adresse e-mail"
              type="email"
              id="email"
              {...register("email", { validate: validateEmail, required: "Ce champ est requis" })}
              error={!!errors.email}
              helperText={errors.email ? errors.email.message : ""}
            />
          </Grid>
          <Grid item xs={12}>
            <TextField
              fullWidth
              label="Mot de passe"
              type="password"
              id="password"
              {...register("password", { required: "Ce champ est requis",
              validate: value => validatePassword(value) })}
              error={!!errors.password}
              helperText={errors.password ? errors.password.message : ""}
            />
          </Grid>
          <Grid item xs={12}>
            <TextField
              fullWidth
              label="Confirmer le mot de passe"
              type="password"
              id="confirmPassword"
              {...register("confirmPassword", { required: "Ce champ est requis",
              validate: value => validateConfirmPassword(value) })}
              error={!!errors.password}
              helperText={errors.password ? errors.password.message : ""}
            />
          </Grid>
        </Grid>
        <Button type="submit" variant="contained" color="primary" sx={{ mt: 2 }}>
          Créer un compte
        </Button>
      </form>
      
    </Container>
  );
}

export default SignUpForm;
