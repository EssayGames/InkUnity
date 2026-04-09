VAR got_key = false
VAR visited = false
VAR taken_quest = false

{got_key:
-true: ->has_key
-false: ->no_key
}

===no_key===
{visited:
-true:Hello again!
-false:Hey there!
}
{taken_quest:
-true:*[>>]->question_key
-false: *[>>]->question_help
}

===question_help===
~visited = true
-Would you mind helping me with something?
*[Sure! #quest_start]->can_help
*[Sorry, I'm busy.]->cant_help

===can_help===
~taken_quest = true
Oh great! I've misplaced my key...
*[>>]
-Could you look for it and bring it back to me?
*[Yup!]
-Great, I might've left it at the top of the lookout, but I can't remember.
*[>>]
-Come back to me once you've got it.
*[END.]->END

===cant_help===
Oh... Ok then. Maybe some other time.
*[END.]->END

===question_key===
Please come back when you got my key!
*[END.]->END

===has_key===
Oh thank you so much! Here's a reward for helping me.
*[\(collect reward\) #advance #reward]->END 

