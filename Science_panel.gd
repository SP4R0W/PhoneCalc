extends Panel

var num1: String = "0"
var num2: String = "0"
var result: String = "0"

const e: float = 2.71828

enum numbers {
	ONE,
	TWO,
	RESULT
}

enum operations {
	ADD,
	SUBTRACT,
	MULTIPLY,
	DIVIDE,
	MOD,
	ROOT,
	POWER,
	NONE
}

var operation_signs = {
	operations.ADD:"+",
	operations.SUBTRACT:"-",
	operations.MULTIPLY:"x",
	operations.DIVIDE:"/",
	operations.ROOT:"RQT",
	operations.POWER:"POW",
}

var current_number = numbers.ONE
var current_operation = operations.NONE

func _process(_delta):
	if (current_number == numbers.ONE):
		$Display_normal/label_action.text = ""
		$Display_normal/label_num.text = num1
	elif (current_number == numbers.TWO):
		$Display_normal/label_action.text = num1 + " " + operation_signs[current_operation]
		$Display_normal/label_num.text = num2
	elif (current_number == numbers.RESULT):
		$Display_normal/label_action.text = num1 + " " + operation_signs[current_operation] + " "  + num2 + " = "
		$Display_normal/label_num.text = result

func reset_to_default():
	num1 = "0"
	num2 = "0"
	result = "0"
	current_number = numbers.ONE
	current_operation = operations.NONE

func _on_ce_button_pressed():
	match (current_number):
		numbers.ONE:
			num1 = "0"
		numbers.TWO:
			num2 = "0"
		numbers.RESULT:
			reset_to_default()

func _on_c_button_pressed():
	reset_to_default()
	
func _on_del_button_pressed():
	if (current_number == numbers.RESULT):
		var res = result
		reset_to_default()
		
		num1 = res
	
	match (current_number):
		numbers.ONE:
			num1 = num1.substr(0,num1.length() - 1)
			if (num1 == ""):
				num1 = "0"
		numbers.TWO:
			num2 = num2.substr(0,num2.length() - 1)
			if (num2 == ""):
				num2 = "0"

func add_num(number: String):
	if (current_number == numbers.RESULT):
		reset_to_default()	
		
	if ($Display_normal/label_num.text.length() > 15):
		return
	
	match (current_number):
		numbers.ONE:
			if (num1 == "0"):
				num1 = ""
				
			num1 += number
		numbers.TWO:
			if (num2 == "0"):
				num2 = ""
			num2 += number
			

func _on_0_button_pressed():
	add_num("0")

func _on_1_button_pressed():
	add_num("1")

func _on_2_button_pressed():
	add_num("2")

func _on_3_button_pressed():
	add_num("3")
	
func _on_4_button_pressed():
	add_num("4")

func _on_5_button_pressed():
	add_num("5")
	
func _on_6_button_pressed():
	add_num("6")

func _on_7_button_pressed():
	add_num("7")

func _on_8_button_pressed():
	add_num("8")

func _on_9_button_pressed():
	add_num("9")

func _on_dot_button_pressed():
	if (current_number == numbers.RESULT):
		var res = result
		reset_to_default()
		
		num1 = res
	
	match (current_number):
		numbers.ONE:
			if (num1.find(".") == -1):
				num1 += "."
		numbers.TWO:
			if (num2.find(".") == -1):
				num2 += "."

func _on_plusmin_button_pressed():
	if (current_number == numbers.RESULT):
		var res = result
		reset_to_default()
		
		num1 = res
	
	match (current_number):
		numbers.ONE:
			num1 = str(float(num1) * -1)
		numbers.TWO:
			num2 = str(float(num2) * -1)
			
func do_equasion() -> String:
	if (current_operation == operations.ADD):
		return str(float(num1) + float(num2))
	elif (current_operation == operations.SUBTRACT):
		return str(float(num1) - float(num2))
	elif (current_operation == operations.MULTIPLY):
		return str(float(num1) * float(num2))
	elif (current_operation == operations.DIVIDE):
		return str(float(num1) / float(num2))
	
	return "0"
	
func set_operation(operation):
	if (current_operation == operations.NONE):
		current_operation = operation
		current_number = numbers.TWO
	else:
		if (current_number == numbers.TWO):
			var res = do_equasion()
			reset_to_default()
			
			num1 = res
			current_operation = operation
			current_number = numbers.TWO
		elif (current_number == numbers.RESULT):
			var res = result
			reset_to_default()
		
			num1 = res
			current_operation = operation
			current_number = numbers.TWO	

func _on_equal_button_pressed():
	if (current_number == numbers.ONE):
		return
		
	current_number = numbers.RESULT
	result = do_equasion()

func _on_add_button_pressed():
	set_operation(operations.ADD)

func _on_min_button_pressed():
	set_operation(operations.SUBTRACT)

func _on_mul_button_pressed():
	set_operation(operations.MULTIPLY)

func _on_div_button_pressed():
	set_operation(operations.DIVIDE)

func _on_sqr_button_pressed():
	if (current_number == numbers.RESULT):
		var res = result
		reset_to_default()
		
		num1 = res
	
	match (current_number):
		numbers.ONE:
			num1 = str(sqrt(float(num1)))
		numbers.TWO:
			num2 = str(sqrt(float(num2)))


func _on_pow_2_button_pressed():
	if (current_number == numbers.RESULT):
		var res = result
		reset_to_default()
		
		num1 = res
	
	match (current_number):
		numbers.ONE:
			num1 = str(pow(float(num1),2))
		numbers.TWO:
			num2 = str(pow(float(num2),2))

func _on_pow_3_button_pressed():
	if (current_number == numbers.RESULT):
		var res = result
		reset_to_default()
		
		num1 = res
	
	match (current_number):
		numbers.ONE:
			num1 = str(pow(float(num1),3))
		numbers.TWO:
			num2 = str(pow(float(num2),3))

func _on_abs_button_pressed():
	if (current_number == numbers.RESULT):
		var res = result
		reset_to_default()
		
		num1 = res
	
	match (current_number):
		numbers.ONE:
			num1 = str(abs(float(num1)))
		numbers.TWO:
			num2 = str(abs(float(num2)))

func _on_divx_button_pressed():
	if (current_number == numbers.RESULT):
		var res = result
		reset_to_default()
		
		num1 = res
	
	match (current_number):
		numbers.ONE:
			num1 = str(1/float(num1))
		numbers.TWO:
			num2 = str(1/float(num2))
			
func _on_ex_button_pressed():
	if (current_number == numbers.RESULT):
		var res = result
		reset_to_default()
		
		num1 = res
	
	match (current_number):
		numbers.ONE:
			num1 = str(exp(float(num1)))
		numbers.TWO:
			num2 = str(exp(float(num2)))
			
func _on_n_button_pressed():
	if (current_number == numbers.RESULT):
		var res = result
		reset_to_default()
		
		num1 = res
	
	match (current_number):
		numbers.ONE:
			var a = 1
			var b = float(num2)
			num1 = str(exp(float(num1)))
		numbers.TWO:
			num2 = str(exp(float(num2)))
		

func _on_rand_button_pressed():
	if (current_number == numbers.RESULT):
		var res = result
		reset_to_default()
	
	match (current_number):
		numbers.ONE:
			num1 = str(randf())
		numbers.TWO:
			num2 = str(randf())


func _on_pi_button_pressed():
	if (current_number == numbers.RESULT):
		var res = result
		reset_to_default()
	
	match (current_number):
		numbers.ONE:
			num1 = str(PI)
		numbers.TWO:
			num2 = str(PI)


func _on_e_button_pressed():
	if (current_number == numbers.RESULT):
		var res = result
		reset_to_default()
	
	match (current_number):
		numbers.ONE:
			num1 = str(e)
		numbers.TWO:
			num2 = str(e)

func _on_round_button_pressed():
	if (current_number == numbers.RESULT):
		var res = result
		reset_to_default()
		
		num1 = res
	
	match (current_number):
		numbers.ONE:
			num1 = str(round(float(num1)))
		numbers.TWO:
			num2 = str(round(float(num2)))

func _on_ceil_button_pressed():
	if (current_number == numbers.RESULT):
		var res = result
		reset_to_default()
		
		num1 = res
	
	match (current_number):
		numbers.ONE:
			num1 = str(ceil(float(num1)))
		numbers.TWO:
			num2 = str(ceil(float(num2)))

func _on_floor_button_pressed():
	if (current_number == numbers.RESULT):
		var res = result
		reset_to_default()
		
		num1 = res
	
	match (current_number):
		numbers.ONE:
			num1 = str(floor(float(num1)))
		numbers.TWO:
			num2 = str(floor(float(num2)))

