extends Control

enum calculators {
	NORMAL,
	SCIENCE,
	PROGRAMMER,
	DATE
}

var calc_type = calculators.NORMAL

func _ready() -> void:
	$Normal_panel.show()

func _process(_delta) -> void:
	match (calc_type):
		calculators.NORMAL:
			$Normal_panel.show()
			$Science_panel.hide()
			$Topside_panel/VBoxContainer/Label.text = "Standardowy"	
		calculators.SCIENCE:
			$Normal_panel.hide()
			$Science_panel.show()
			$Topside_panel/VBoxContainer/Label.text = "Naukowy"	
		calculators.PROGRAMMER:
			$Topside_panel/VBoxContainer/Label.text = "Programisty"	
		calculators.DATE:
			$Topside_panel/VBoxContainer/Label.text = "Kalkulator dat"	


func _on_Hamburger_button_pressed():
	$Hamburger_panel/Tween.remove_all()
	
	if (is_equal_approx($Hamburger_panel.rect_global_position.x,-540)):
		$Hamburger_panel/Tween.interpolate_property($Hamburger_panel,"rect_global_position:x",-540,0,1,Tween.TRANS_SINE,Tween.EASE_IN_OUT)
	elif (is_zero_approx($Hamburger_panel.rect_global_position.x)):
		$Hamburger_panel/Tween.interpolate_property($Hamburger_panel,"rect_global_position:x",0,-540,1,Tween.TRANS_SINE,Tween.EASE_IN_OUT)
	else:
		$Hamburger_panel/Tween.interpolate_property($Hamburger_panel,"rect_global_position:x",rect_global_position.x,-540,0.5,Tween.TRANS_SINE,Tween.EASE_IN_OUT)
		
	$Hamburger_panel/Tween.start()


func _on_Standard_pressed():
	calc_type = calculators.NORMAL
	
	$Hamburger_panel/Tween.interpolate_property($Hamburger_panel,"rect_global_position:x",rect_global_position.x,-540,0.5,Tween.TRANS_SINE,Tween.EASE_IN_OUT)
	$Hamburger_panel/Tween.start()

func _on_Science_pressed():
	calc_type = calculators.SCIENCE
	$Hamburger_panel/Tween.interpolate_property($Hamburger_panel,"rect_global_position:x",rect_global_position.x,-540,0.5,Tween.TRANS_SINE,Tween.EASE_IN_OUT)
	$Hamburger_panel/Tween.start()
