using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace pendu_guerraz
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Création d'un timer pour gérer le temps
        private DispatcherTimer timer;
        private TimeSpan timeLeft = TimeSpan.FromMinutes(3) + TimeSpan.FromSeconds(30);

        public MainWindow()
        {
            InitializeComponent();
            MOD();       // Appel de la fonction MOD() pour initialiser certains éléments.
        }

        // Déclaration des variables
        string motchoisi = "";
        int secret = 0;
        int verif = 0;
        int nomimage = 1;
        int gagne = 0;
        int therme = 0;
        int modee = -1;
        string motdevine = "";
        string easter = "";
        string motsauvegarder = "";

        // Liste de mots simples
        string[] ListMot =  {
            "CHIEN", "CHAT", "SOLEIL", "FLEUR", "ARBRE",
            "MAISON", "VOITURE", "MER", "MONTAGNE", "ETOILE",
            "PLANTE", "OISEAU", "LIVRE", "TABLE", "PORTE",
            "FENETRE", "CUISINE", "JARDIN", "FRUIT", "LEGUME",
            "RUE", "ECOLE", "FETE", "MUSIQUE", "FILM",
            "CIEL", "NUAGE", "VENT", "RIVIERE", "PONT",
            "PAYSAGE", "VOYAGE", "CADEAU", "COULEUR", "BONHEUR",
            "SOURIRE", "AMOUR", "AMI", "FAMILLE", "TEMPS",
            "MATIN", "SOIR", "NUIT", "JOUR", "ANNEE",
            "HEURE", "MINUTE", "SECOND", "CALME", "PAIX",
            "ESPOIR", "REVE", "RIRE", "JEU", "SPORT",
            "PLAGE", "VACANCES", "PHOTO", "FENETRE", "TABLEAU",
            "POEME", "LETTRE", "MOT", "PHRASE", "SILENCE",
            "PAROLE", "PENSEE", "QUESTION", "REPONSE", "IDEE",
            "TRAVAIL", "ARGENT", "SANTE", "JOIE", "PEINE",
            "DOUCEUR", "FORCE", "COURAGE", "VALEUR", "HONNEUR",
            "RESPECT", "SAGESSE", "LIBERTE", "JUSTICE", "VERITE"
            };

        // Liste de mots difficiles
        string[] ListMotdur = {
            "ANTICONSTITUTIONNELLEMENT", "HIPPOPOTOMONSTROSESQUIPPEDALIOPHOBIE", "INCOMMENSURABLE", "ELECTROENCEPHALOGRAPHIE", "SUPERCALIFRAGILISTICEXPIALIDOCIOUS",
            "PNEUMONOULTRAMICROSCOPICSILICOVOLCANOCONIOSE", "INTERDISCIPLINARITE", "EQUIVALENCE", "INDIVISIBILITE", "THERMODYNAMIQUE",
            "DURABILITE", "PHILOPROGENITIVITE", "EPIDEMIOLOGIE", "SISMOTHERAPIE", "ANTICONTRADICTOIREMENT",
            "MEGALOMANIE", "INDOMPTABILITE", "DEMULTIPLICATEUR", "PERSEVERANT", "EPHEMERIDITE",
            "ONOMATOPEE", "INEXTRICABLE", "SURREALISTE", "BIBLIOTHECAIRE", "QUINTESSENCE",
            "PHOSPHORESCENCE", "CACOPHONIE", "ENIGMATIQUE", "UBIQUITE", "PROLIFIQUE",
            "AMBIGUITE", "CIRCUMLOCUTION", "EXACERBATION", "ECLECTIQUE", "METAMORPHOSE",
            "HETEROGENEITE", "SERENDIPITE", "INEFFABLE", "UBUESQUE"
        };

        // Liste de mots inversés
        string[] ListMotinverse = {
               "ETIREV" ,"ECITSUJ" ,"ETREBIL" ,"ESSEGAS" ,"TCEPSER"
                ,"RUENNOH" ,"RUELAV" ,"EGARUOC" ,"ECROF" ,"RUECUOD"
                ,"ENIEP" ,"EIOJ" ,"ETNAS" ,"TNEGRA" ,"LIAVART"
                ,"EEDI" ,"ESNOPER" ,"NOITSEUQ" ,"EESNEP" ,"ELORAP"
                ,"ECNELIS" ,"ESARHP" ,"TOM" ,"ERTTEL" ,"EMEOP"
                ,"UAELBAT" ,"ERTENEF" ,"OTOHP" ,"SECNACAV" ,"EGALP"
                ,"TROPS" ,"UEJ" ,"ERIR" ,"EVER" ,"RIOPSE"
                ,"XIAP" ,"EMLAC" ,"DNOCES" ,"ETUNIM" ,"ERUEH"
                ,"EENNA" ,"RUOJ" ,"TIUN" ,"RIOS" ,"NITAM"
                ,"SPMET" ,"ELLIMAF" ,"IMA" ,"RUOMA" ,"ERIRUOS"
                ,"RUEHNOB" ,"RUELUOC" ,"UAEDAC" ,"EGAYOV" ,"EGASYAP"
                ,"TNOP" ,"EREIVIR" ,"TNEV" ,"EGAUN" ,"LEIC"
                ,"MLIF" ,"EUQISUM" ,"ETEF" ,"ELOCE" ,"EUR"
                ,"EMUGEL" ,"TIURF" ,"NIDRAJ" ,"ENISIUC" ,"ERTENEF"
                ,"ETROP" ,"ELBAT" ,"ERVIL" ,"UAESIO" ,"ETNALP"
                ,"ELIOTE" ,"ENGATNOM" ,"REM" ,"ERUTIOV" ,"NOSIAM"
                ,"ERBRA" ,"RUELF" ,"LIELOS" ,"TAHC" ,"NEIHC"

           };



        // Initialisation de l'objet Random pour générer des nombres aléatoires
        Random random = new Random();

        // Méthode déclenchée lorsqu'un bouton est cliqué
        private void BTN_Click(object sender, RoutedEventArgs e)
        {
            // Conversion du sender en objet Button
            Button btn = sender as Button;

            // Obtention du contenu du bouton (le texte affiché)
            string btnContent = btn.Content.ToString();

            // Désactivation du bouton après qu'il a été cliqué pour éviter les clics multiples
            btn.IsEnabled = false;


            // Vérification du statut du jeu (si secret est 0, cela signifie que l'easter egg est activé)
                if (secret == 0)
            {
                // Appel de la fonction easteregg avec le contenu du bouton
                easteregg(btnContent);
            }

            // Si le secret est 1, le jeu est en cours
            else if (secret == 1)
            {
                // Appel de la fonction de vérification des mots avec le contenu du bouton
                verificationMots(btnContent);

            }    
        }

        //fonction verification
        public async void verificationMots(string lettre)
        {

            //temps que i est plus petit que le nombre de lettre 
            for (int i = 0; i < motchoisi.Length; i++)
            {
                //si la lettre est la meme que celle qu'on a choisit
                if (motchoisi[i].ToString() == lettre)
                {
                    motsauvegarder = motdevine;
                    motdevine = "";

                    for (int t = 0; t < motchoisi.Length; t++)
                    {

                        //si c'est a la bonne position
                        if (t == i)
                        {
                            motdevine = motdevine + lettre;
                            verif++;
                        }

                        //sinon si c'est une etoile
                        else if (motsauvegarder[t] == '*')
                        {
                            motdevine = motdevine + "*";
                        }

                        // si c'est une lettre
                        else
                        {
                            motdevine = motdevine + motsauvegarder[t];
                        }
                    }

                    //affiche le mot
                    TB_affichage.Text = motdevine;
                }


            }

            //regarde si il faut enlever une vie
            if (verif == 0)
            {
                // Vérifie si le nombre d'images affichées est inférieur à 6
                if (nomimage < 6)
                {
                    // Incrémente le nombre d'images pour afficher la prochaine image de vie
                    nomimage++;

                    // Affiche l'image correspondante au nombre d'images
                    displayImage(nomimage.ToString());

                    // Si l'easter egg est activé avec le code "MIB"
                    if (easter == "MIB")
                    {

                        // Désactive tous les boutons pendant 10 secondes
                        foreach (Button btn in chose2.Children.OfType<Button>())
                        {
                            btn.IsEnabled = false;
                        }

                        // Attend pendant 10 secondes (async)
                        await Task.Delay(10000);

                        // Réactive tous les boutons après l'attente
                        foreach (Button btn in chose2.Children.OfType<Button>())
                        {
                            btn.IsEnabled = true;
                        }
                    }
                }

                //si il y a trop d'erreure il a perdue
                else
                {
                    // Affiche un message indiquant que le joueur a perdu
                    TB_affichage.Text = "  vous avez perdu \n appuyer sur restart\nvous pouvez changer de mode";

                    // Réactive le bouton de sélection de mode
                    mode.IsEnabled = true;

                    // Si l'easter egg "TIMER" ou "MIB" est activé, arrête le timer
                    if (easter == "TIMER" || easter == "MIB")
                    {
                        StopTimer();
                    }
                    // Réactive tous les boutons
                    foreach (Button btn in chose2.Children.OfType<Button>())
                    {
                        btn.IsEnabled = true;
                    }
                    // Réactive le bouton de redémarrage
                    RESTART.IsEnabled = true;

                    // Réinitialise le statut du jeu, l'easter egg et les erreurs
                    secret = 0;
                    easter = "";

                }
            }

            // Réinitialise la variable de vérification des erreurs
            verif = 0;

            // Vérifie si le joueur a gagné en comparant le mot deviné avec le mot choisi
            for (int i = 0; i < motchoisi.Length; i++)
            {
                // Si la lettre a été devinée (n'est pas un astérisque), incrémente le compteur de victoires
                if (motdevine[i] != '*')
                {
                    gagne++;
                }
            }

            // Si le nombre de lettres devinées est égal à la longueur du mot choisi, le joueur a gagné
            if (motdevine.Length == gagne)
            {
                // Affiche un message de victoire
                TB_affichage.Text = motdevine + "\n vous avez gagné\npour rejouer appuyer sur restart\nvous pouvez changer de mode";

                // Réactive le bouton de sélection de mode
                mode.IsEnabled = true;

                // Si l'easter egg "TIMER" ou "MIB" est activé, arrête le timer
                if (easter == "TIMER" || easter == "MIB")
                {
                    StopTimer();
                }

                // Réactive tous les boutons
                foreach (Button btn in chose2.Children.OfType<Button>())
                {
                    btn.IsEnabled = true;
                }

                // Réactive le bouton de redémarrage
                RESTART.IsEnabled = true;

                // Réinitialise le statut du jeu et l'easter egg
                secret = 0;
                easter = "";
            }

            // Réinitialise le compteur de victoires pour une utilisation future
            gagne = 0;
        }

        //fonction image
        public void displayImage(string nomimage)
        {
            Uri resourceUri = new Uri("/image/" + nomimage + ".png", UriKind.Relative);
            Img_Pendu.Source = new BitmapImage(resourceUri);
        }

        //quand restarte apuyer
        private void RESTRAT_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
            if(easter == "TIMER" || easter == "MIB")
                {
                temps.Visibility = Visibility.Hidden;
                StopTimer();
                easter = "";
                 }
        }

        //quand starte apuyer
        private void START_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }

        // fonction pour changer le mode de jeu
        public void MOD()
        {
            // Incrémente le compteur de mode
            modee++;

            // Si le compteur de mode dépasse 2, réinitialise à 0
            if (modee > 2)
            {
                modee = 0;
            }

            // Sélectionne le texte à afficher en fonction du mode
            if (modee == 0)
            {
                TB_affichage.Text = "mode simple";
            }
            else if (modee == 1)
            {
                TB_affichage.Text = "mode hard";
            }
            else if (modee == 2)
            {
                TB_affichage.Text = "mode inverse";
            }

            // Désactive le bouton RESTART et active le bouton START
            RESTART.IsEnabled = false;
            START.IsEnabled = true;

        }

        // fonction pour gérer l'easter egg
        public async void easteregg(string lettre)
        {
            // Ajoute la lettre reçue à la chaîne de l'easter egg
            easter = easter + lettre;

            // Si l'easter egg "TIMER" est activé
            if (easter == "TIMER")
            {
                // Affiche un message indiquant que l'easter egg est activé
                TB_affichage.Text = "easter egg activer";

                // Attend pendant 2 secondes 
                await Task.Delay(2000);

                // Incrémente le statut du jeu
                secret++;

                // Démarre le jeu et initialise le timer
                StartGame();
                InitializeTimer();

                // Rend le composant temps visible
                temps.Visibility = Visibility.Visible;
            }

            // Si l'easter egg "MIB" est activé
            if (easter == "MIB")
            {
                // Affiche un message indiquant que l'easter egg est activé
                TB_affichage.Text = "easter egg activer";

                // Attend pendant 2 secondes 
                await Task.Delay(2000);

                // Incrémente le statut du jeu
                secret++;

                // Démarre le jeu et initialise le timer
                StartGame();
                InitializeTimer();

                // Rend le composant temps visible
                temps.Visibility = Visibility.Visible;
            }
        }     
 

        //fonction strate
        public void StartGame()
        {
            //tout remetre a zero
            motchoisi = "";
            verif = 0;
            nomimage = 1;
            gagne = 0;
            therme = 0;
            secret = 1;
            motdevine = "";
            motsauvegarder = "";
            Uri resourceUri = new Uri("/image/" + "1.png", UriKind.Relative);
            Img_Pendu.Source = new BitmapImage(resourceUri);
            ResetTimer();
            temps.Visibility = Visibility.Hidden;

            mode.IsEnabled = false;
            RESTART.IsEnabled = true;
            START.IsEnabled = false;

            foreach (Button btn in chose2.Children.OfType<Button>())
            {
                btn.IsEnabled = true;
            }

            if (modee == 0)
            {
                //le chifre aleatoire va dans la variable therme
                therme = random.Next(0, ListMot.Length);

                //le mot dans la liste va dans la variable liste
                motchoisi = ListMot[therme];
            }
          
            else if (modee == 1)
            {
                //le chifre aleatoire va dans la variable therme
                therme = random.Next(0, ListMotdur.Length);

                //le mot dans la liste va dans la variable liste
                motchoisi = ListMotdur[therme];
            }

            else if (modee == 2)
            {
                //le chifre aleatoire va dans la variable therme
                therme = random.Next(0, ListMotinverse.Length);

                //le mot dans la liste va dans la variable liste
                motchoisi = ListMotinverse[therme];

            }

            //metre le nombre de lettre
            for (int i = 0; i < motchoisi.Length; i++)
            {
                motdevine = motdevine + "*";
            }

            TB_affichage.Text = motdevine;

        }

        public void mode_Click(object sender, RoutedEventArgs e)
        {
            MOD();
        }


        public void InitializeTimer()
        { //configuration du timer
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); //intervalle de temps de 1 s
            timer.Tick += Timer_Tick;
            timer.Start();

        }

        //demarrer le timer
        public void StartTimer()
        {
            timer.Start();
        }
        //mettre en pause le timer
        public void StopTimer()
        {
            timer.Stop();
        }
        //reset du timer
        public void ResetTimer()
        {
            timeLeft = TimeSpan.FromMinutes(3) + TimeSpan.FromSeconds(30);  // Réinitialise le temps restant
            temps.Text = timeLeft.ToString(@"mm\:ss");  // Met à jour l'affichage du temps
        }

        public void Timer_Tick(object sender, EventArgs e)
        {
            if (timeLeft.TotalSeconds > 0)
            {
                timeLeft = timeLeft.Subtract(TimeSpan.FromSeconds(1));
                temps.Text = timeLeft.ToString(@"mm\:ss"); // Met à jour le Label avec le temps restant
            }
            else
            {
                timer.Stop();
                TB_affichage.Text = "  vous avez perdu \n appuyer sur restart\nvous pouvez changer de mode";
                mode.IsEnabled = true;
                easter = "";
                foreach (Button btn in chose2.Children.OfType<Button>())
                {
                    btn.IsEnabled = true;
                }
                RESTART.IsEnabled = true;
                secret = 0;


                // Réinitialise le jeu
            }
        }

    }
}