extends Control

enum calculators {
	NORMAL,
	SCIENCE,
	DATE,
	CONVERT
}

var calc_type = calculators.NORMAL

func _ready() -> void:
	$Normal_panel.show()

func _process(_delta) -> void:
	if (calc_type != calculators.CONVERT):
		$Currency_Panel.hide()
		$Volume_Panel.hide()
		$Length_Panel.hide()	
		$Mass_Panel.hide()		
		$Temperature_Panel.hide()
		$Energy_Panel.hide()		
		$Area_Panel.hide()	
		$Velocity_Panel.hide()
		$Time_Panel.hide()
		$Power_Panel.hide()
		$Data_Panel.hide()
		$Pressure_Panel.hide()
		$Angle_Panel.hide()
	
	match (calc_type):
		calculators.NORMAL:
			$Normal_panel.show()
			$Science_panel.hide()
			$Date_Panel.hide()			
			$Topside_panel/VBoxContainer/Label.text = "Standardowy"	
		calculators.SCIENCE:
			$Normal_panel.hide()
			$Science_panel.show()
			$Date_Panel.hide()
			$Topside_panel/VBoxContainer/Label.text = "Naukowy"	
		calculators.DATE:
			$Normal_panel.hide()
			$Science_panel.hide()			
			$Date_Panel.show()
			$Topside_panel/VBoxContainer/Label.text = "Kalkulator dat"	
		calculators.CONVERT:
			$Topside_panel/VBoxContainer/Label.text = "Konwerter"


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

func _on_Date_pressed():
	calc_type = calculators.DATE
	$Hamburger_panel/Tween.interpolate_property($Hamburger_panel,"rect_global_position:x",rect_global_position.x,-540,0.5,Tween.TRANS_SINE,Tween.EASE_IN_OUT)
	$Hamburger_panel/Tween.start()

func _on_Currency_pressed():
	calc_type = calculators.CONVERT
	$Hamburger_panel/Tween.interpolate_property($Hamburger_panel,"rect_global_position:x",rect_global_position.x,-540,0.5,Tween.TRANS_SINE,Tween.EASE_IN_OUT)
	$Hamburger_panel/Tween.start()
	
	$Normal_panel.hide()
	$Science_panel.hide()
	
	$Currency_Panel.show()
	
func _on_Volume_pressed():
	calc_type = calculators.CONVERT
	$Hamburger_panel/Tween.interpolate_property($Hamburger_panel,"rect_global_position:x",rect_global_position.x,-540,0.5,Tween.TRANS_SINE,Tween.EASE_IN_OUT)
	$Hamburger_panel/Tween.start()
	
	$Normal_panel.hide()
	$Science_panel.hide()
	
	$Volume_Panel.show()


func _on_Length_pressed():
	calc_type = calculators.CONVERT
	$Hamburger_panel/Tween.interpolate_property($Hamburger_panel,"rect_global_position:x",rect_global_position.x,-540,0.5,Tween.TRANS_SINE,Tween.EASE_IN_OUT)
	$Hamburger_panel/Tween.start()
	
	$Normal_panel.hide()
	$Science_panel.hide()
	
	$Length_Panel.show()

func _on_Mass_pressed():
	calc_type = calculators.CONVERT
	$Hamburger_panel/Tween.interpolate_property($Hamburger_panel,"rect_global_position:x",rect_global_position.x,-540,0.5,Tween.TRANS_SINE,Tween.EASE_IN_OUT)
	$Hamburger_panel/Tween.start()
	
	$Normal_panel.hide()
	$Science_panel.hide()
	
	$Mass_Panel.show()

func _on_Temperature_pressed():
	calc_type = calculators.CONVERT
	$Hamburger_panel/Tween.interpolate_property($Hamburger_panel,"rect_global_position:x",rect_global_position.x,-540,0.5,Tween.TRANS_SINE,Tween.EASE_IN_OUT)
	$Hamburger_panel/Tween.start()
	
	$Normal_panel.hide()
	$Science_panel.hide()
	
	$Temperature_Panel.show()
	
func _on_Energy_pressed():
	calc_type = calculators.CONVERT
	$Hamburger_panel/Tween.interpolate_property($Hamburger_panel,"rect_global_position:x",rect_global_position.x,-540,0.5,Tween.TRANS_SINE,Tween.EASE_IN_OUT)
	$Hamburger_panel/Tween.start()
	
	$Normal_panel.hide()
	$Science_panel.hide()
	
	$Energy_Panel.show()

func _on_Surface_pressed():
	calc_type = calculators.CONVERT
	$Hamburger_panel/Tween.interpolate_property($Hamburger_panel,"rect_global_position:x",rect_global_position.x,-540,0.5,Tween.TRANS_SINE,Tween.EASE_IN_OUT)
	$Hamburger_panel/Tween.start()
	
	$Normal_panel.hide()
	$Science_panel.hide()
	
	$Area_Panel.show()
	
func _on_Velocity_pressed():
	calc_type = calculators.CONVERT
	$Hamburger_panel/Tween.interpolate_property($Hamburger_panel,"rect_global_position:x",rect_global_position.x,-540,0.5,Tween.TRANS_SINE,Tween.EASE_IN_OUT)
	$Hamburger_panel/Tween.start()
	
	$Normal_panel.hide()
	$Science_panel.hide()
	
	$Velocity_Panel.show()
	
func _on_Time_pressed():
	calc_type = calculators.CONVERT
	$Hamburger_panel/Tween.interpolate_property($Hamburger_panel,"rect_global_position:x",rect_global_position.x,-540,0.5,Tween.TRANS_SINE,Tween.EASE_IN_OUT)
	$Hamburger_panel/Tween.start()
	
	$Normal_panel.hide()
	$Science_panel.hide()
	
	$Time_Panel.show()
	
func _on_Power_pressed():
	calc_type = calculators.CONVERT
	$Hamburger_panel/Tween.interpolate_property($Hamburger_panel,"rect_global_position:x",rect_global_position.x,-540,0.5,Tween.TRANS_SINE,Tween.EASE_IN_OUT)
	$Hamburger_panel/Tween.start()
	
	$Normal_panel.hide()
	$Science_panel.hide()
	
	$Power_Panel.show()
	
func _on_Data_pressed():
	calc_type = calculators.CONVERT
	$Hamburger_panel/Tween.interpolate_property($Hamburger_panel,"rect_global_position:x",rect_global_position.x,-540,0.5,Tween.TRANS_SINE,Tween.EASE_IN_OUT)
	$Hamburger_panel/Tween.start()
	
	$Normal_panel.hide()
	$Science_panel.hide()
	
	$Data_Panel.show()

func _on_Pressure_pressed():
	calc_type = calculators.CONVERT
	$Hamburger_panel/Tween.interpolate_property($Hamburger_panel,"rect_global_position:x",rect_global_position.x,-540,0.5,Tween.TRANS_SINE,Tween.EASE_IN_OUT)
	$Hamburger_panel/Tween.start()
	
	$Normal_panel.hide()
	$Science_panel.hide()
	
	$Pressure_Panel.show()

func _on_Angle_pressed():
	calc_type = calculators.CONVERT
	$Hamburger_panel/Tween.interpolate_property($Hamburger_panel,"rect_global_position:x",rect_global_position.x,-540,0.5,Tween.TRANS_SINE,Tween.EASE_IN_OUT)
	$Hamburger_panel/Tween.start()
	
	$Normal_panel.hide()
	$Science_panel.hide()
	
	$Angle_Panel.show()

