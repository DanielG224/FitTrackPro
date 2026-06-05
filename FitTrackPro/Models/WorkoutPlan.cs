namespace FitTrackPro.Models
{
    public class WorkoutPlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Level { get; set; }

        public ICollection<WorkoutPlanExercise> WorkoutPlanExercises { get; set; }
            = new List<WorkoutPlanExercise>();
    }
}