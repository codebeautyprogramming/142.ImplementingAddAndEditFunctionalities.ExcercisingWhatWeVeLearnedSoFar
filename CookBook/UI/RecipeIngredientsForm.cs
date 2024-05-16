using CookBook.ViewModels;
using DataAccessLayer.Contracts;
using DataAccessLayer.CustomQueryResults;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CookBook.UI
{
    public partial class RecipeIngredientsForm : Form
    {
        private readonly IRecipeIngredientsRepository _recipeIngredientsRepository;
        private readonly IIngredientsRepository _ingredientsRepository;

        public int RecipeId { get; set; }
        public RecipeIngredientsForm(IRecipeIngredientsRepository recipeIngredientsReposiotry, IIngredientsRepository ingredientsRepository)
        {
            InitializeComponent();
            _recipeIngredientsRepository = recipeIngredientsReposiotry;
            _ingredientsRepository = ingredientsRepository;
        }

        private void RecipeIngredientsForm_Load(object sender, EventArgs e)
        {
            RefreshRecipeIngredients();
            RefreshAllIngredients();
        }

        private async void RefreshRecipeIngredients()
        {
            List< RecipeIngredientWithNameAndAmount > results = await _recipeIngredientsRepository.GetRecipeIngredients(RecipeId);
            List<RecipeIngredientVM> recipeIngredients= new List<RecipeIngredientVM>();

            foreach(var ingredient in results) {
                recipeIngredients.Add(new RecipeIngredientVM(ingredient.IngredientId, ingredient.Name, ingredient.Amount));
            }

            RecipeIngredientsLbx.DataSource=recipeIngredients;
            RecipeIngredientsLbx.DisplayMember = "NameWithAmount";
        }
        private async void RefreshAllIngredients()
        {
            AllIngredientsLbx.DataSource = await _ingredientsRepository.GetIngredients();
            AllIngredientsLbx.DisplayMember = "Name";
        }
    }
}
