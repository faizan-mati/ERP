using NEW_ERP.Forms.ItemMasterForms;
using NEW_ERP.GernalClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NEW_ERP.Forms.ItemMaster
{
    public partial class ItemMasterForm : Form
    {
        public ItemMasterForm()
        {
            InitializeComponent();
        }
        //========================================= FORM LOAD EVENT =================================================

        private void ItemMasterForm_Load(object sender, EventArgs e)
        {

        }

        //========================================= SUBMIT BUTTON =================================================

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (isValidation())
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    try
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("sp_InsertItemMaster", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ProductCode", TxtProductCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@ProductDescription", TxtProductDes.Text.Trim());
                        cmd.Parameters.AddWithValue("@ProductShortName", TxtProductShortName.Text.Trim());
                        cmd.Parameters.AddWithValue("@UserCode", "000123");
                        cmd.Parameters.AddWithValue("@Remarks", TxtProductRemarks.Text.Trim());

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Item inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RestFormControler();
                        } 
                        else
                        {
                            MessageBox.Show("Insertion failed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        //======================================= FOR VALIDATION =======================================

        private bool isValidation()
        {
            if (string.IsNullOrWhiteSpace(TxtProductCode.Text) ||
                string.IsNullOrWhiteSpace(TxtProductShortName.Text) ||
                string.IsNullOrWhiteSpace(TxtProductDes.Text)

            )
            {
                MessageBox.Show("Please Fill All the Fields", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        //======================================= REST FORM =======================================

        private void RestFormControler()
        {
            TxtProductCode.Clear();
            TxtProductDes.Clear();
            TxtProductShortName.Clear();
            TxtProductRemarks.Clear();

        }

        //========================================= BUTTONS =================================================

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ViewAllBtn_Click(object sender, EventArgs e)
        {
            ItemMasterViewAll NextForm = new ItemMasterViewAll();
            NextForm.Show();
        }       

        private void EditBtn_Click(object sender, EventArgs e)
        {
            ItemMasterEdit NextForm = new ItemMasterEdit();
            NextForm.Show();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            ItemMasterDelete NextForm = new ItemMasterDelete();
            NextForm.Show();
        }


    }
}






//1.What is Associative learning ?

//Associative learning is a type of learning in which a person or animal makes a connection between two events or stimuli.
//There are two main types of associative learning:

//Classical Conditioning – Learning by associating a neutral stimulus with a meaningful one.

//Example: Pavlov 's dogs learned to associate the sound of a bell with food, so they started salivating just by hearing the bell.

//Operant Conditioning – Learning through consequences (rewards or punishments).

//Example: A student studies hard and gets good grades (reward), so they continue studying hard.

//In short:
//Associative learning is learning that happens when two things are linked together in the mind through experience.





//Jean Piaget’s Theory of Cognitive Development
//Piaget believed that children go through four stages of mental development as they grow. He focused on how children think and understand the world around them.

//The Four Stages:

//Sensorimotor Stage(Birth to 2 years):
//The child learns through senses and actions (touching, looking, sucking).
//Develops object permanence(knowing that things still exist even when out of sight).


//Preoperational Stage(2 to 7 years):
//The child uses language and imagination.
//Thinking is egocentric (they can’t see things from another person’s point of view).
//Cannot yet perform logical operations like conservation.


//Concrete Operational Stage (7 to 11 years):
//Child begins logical thinking but only about concrete (real) objects.
//Understands conservation(e.g., same amount of water in different shaped glasses).
//Learns cause and effect.

//Formal Operational Stage (12 and up): 
//Teen can think abstractly and hypothetically.
//Can solve complex problems using logic.




//How to Reduce Prejudice – Short-Term Steps:

//🔹 1.Be Aware of Your Own Bias:
//Notice when you are thinking unfairly about someone.
//Ask yourself:
// “Why do I think this ?”
// “Is it fair?”

//🔹 2. Learn and Educate Yourself:
//Read or learn about other cultures, religions, or groups.
//Understand where stereotypes come from — and why they’re often wrong.

//🔹 3. Practice Empathy:
//Try to imagine how it feels to be treated badly because of your race, religion, or age.

//🔹 4. Talk About It:
//Have open discussions about prejudice and how it affects people.
//Talking helps remove fear and ignorance.


//How to Reduce Prejudice – Long-Term Steps:

//🔸 1.Spend Time With Different People:
//Meet and become friends with people from different backgrounds.
//This helps break stereotypes and builds understanding.

//🔸 2. Promote Cultural Understanding:
//Schools, media, and community groups can teach respect for all cultures.

//🔸 3. Support Fair Laws:
//Stand up for laws that protect everyone from discrimination in schools, jobs, etc.

//🔸 4. Change Social Values:
//Teach children and others to value fairness, respect, and inclusion.





//Stress and Its Negative Impact on Health
//In Terms of Psychological, Emotional, and Behavioral Aspects:


//Psychological Impact:
//Anxiety – Constant worry and fear about future events.
//Depression – Long-term stress can lead to feelings of sadness, hopelessness, and loss of interest.
//Burnout – Mental exhaustion from prolonged stress.
//Cognitive Issues – Difficulty in concentrating, making decisions, or remembering things.


//Emotional Impact:
//Mood Swings – Rapid changes in emotions like anger, frustration, or sadness.
//Irritability – Getting annoyed or upset easily.
//Low Self-Esteem – Stress can make a person feel inadequate or not good enough.
//Feeling Overwhelmed – Emotionally unable to cope with daily tasks or responsibilities.


//Behavioral Impact:
//Sleep Disturbances – Trouble falling or staying asleep.
//Unhealthy Eating – Overeating, undereating, or craving junk food.
//Withdrawal – Avoiding friends, family, or activities.
//Substance Use – Increased use of alcohol, smoking, or drugs to cope.
//Physical Tics or Reactions – Grinding teeth, trembling, muscle tension.




//Benefits of Client-Centered Therapy
//Client-centered therapy (also called person-centered or Rogerian therapy) focuses on 
//helping individuals become more self-aware and self-accepting. Here are the key benefits:

//1.Improved Self - Concept
//Helps people develop a healthier and more realistic view of themselves.
//Reduces incongruence (mismatch between self-image and reality).
//Example: A person may view themselves as boring, but therapy helps them see they are actually interesting and engaging.

//2. Increased Self-Esteem
//By creating a non-judgmental and accepting environment, people feel valued and respected.
//This leads to better self-confidence and emotional healing.

//3. Emotional Safety and Growth
//The therapist uses:
//Genuineness
//Unconditional positive regard
//Empathetic understanding
//These techniques make clients feel safe, supported, and understood — which encourages growth.

//4. Self-Direction and Independence
//The client leads the conversation, not the therapist.
//This encourages independent thinking and personal responsibility.




//Prejudice – Simple Definition:
//Prejudice means having a negative opinion or attitude about a group of people (like a race, religion, or gender) without really knowing them.
//It is usually based on stereotypes — simple and often wrong ideas about a group.
//When someone acts on their prejudice, it becomes discrimination, which means treating people unfairly just because they belong to a certain group.

//How to Reduce Prejudice – Short-Term Steps:
//🔹 1.Be Aware of Your Own Bias:
//Notice when you are thinking unfairly about someone.
//Ask yourself:
// “Why do I think this ?”
// “Is it fair?”

//🔹 2. Learn and Educate Yourself:
//Read or learn about other cultures, religions, or groups.
//Understand where stereotypes come from — and why they’re often wrong.

//🔹 3. Practice Empathy:
//Try to imagine how it feels to be treated badly because of your race, religion, or age.

//🔹 4. Talk About It:
//Have open discussions about prejudice and how it affects people
//Talking helps remove fear and ignorance.


//How to Reduce Prejudice – Long-Term Steps:
//🔸 1.Spend Time With Different People:
//Meet and become friends with people from different backgrounds.
//This helps break stereotypes and builds understanding.

//🔸 2. Promote Cultural Understanding:
//Schools, media, and community groups can teach respect for all cultures.

//🔸 3. Support Fair Laws:
//Stand up for laws that protect everyone from discrimination in schools, jobs, etc.

//🔸 4. Change Social Values:
//Teach children and others to value fairness, respect, and inclusion.



//Bipolar Disorder – Explanation
//Definition:
//Bipolar disorder is a mental health condition where a person experiences extreme mood swings — from very high (mania) to very low (depression).
//These mood swings affect:
//Emotions
//Thoughts
//Behavior
//Sleep
//Energy levels

//🔁 Two Main Phases of Bipolar Disorder:
//1.Mania(or Manic Episode):
//In this phase, a person feels:

//Very happy, excited, or high
//Irritable or overly confident
//Talks very fast and has racing thoughts
//Takes on many activities or risky tasks
//Sleeps little but feels energetic
//May do reckless things (spending too much money, dangerous decisions)


//2. Depression (or Depressive Episode):
//In this phase, a person feels:

//Very sad or hopeless for a long time
//Loses interest in activities
//Feels tired or “slowed down”
//Has trouble concentrating or making decisions
//May change eating or sleeping habits
//Might have thoughts of death or suicide
