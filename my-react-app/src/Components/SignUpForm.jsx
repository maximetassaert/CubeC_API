import React from "react";
import { useForm } from "react-hook-form";
import { Button, TextField, Typography, Container, Grid } from "@mui/material";


function SignUpForm() {
  const { register, handleSubmit, formState: { errors } } = useForm();

  const onSubmit = (data) => {
    // Envoyer les données du formulaire au serveur ou effectuer d'autres actions
    console.log(data);
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
              {...register("email", { required: true })}
              error={!!errors.email}
              helperText={errors.email ? "Ce champ est requis" : ""}
            />
          </Grid>
          <Grid item xs={12}>
            <TextField
              fullWidth
              label="Mot de passe"
              type="password"
              id="password"
              {...register("password", { required: true })}
              error={!!errors.password}
              helperText={errors.password ? "Ce champ est requis" : ""}
            />
          </Grid>
          <Grid item xs={12}>
            <TextField
              fullWidth
              label="Confirmer le mot de passe"
              type="password"
              id="confirmPassword"
              {...register("confirmPassword", { required: true })}
              error={!!errors.confirmPassword}
              helperText={errors.confirmPassword ? "Ce champ est requis" : ""}
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
