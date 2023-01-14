import requests
import sys
import json

currencies = ["PLN","USD","EUR","GBP","CNY","JPY","RUB","UAH"]

values = {}

for currency in currencies:
    try:
        url = f'https://api.exchangerate.host/latest?base={currency}&symbols=PLN,USD,EUR,GBP,CNY,JPY,RUB,UAH&places=2'
        response = requests.get(url)
        values[currency] = response.json()['rates']
    except:
        sys.exit("Couldn't fetch data.")

with open("currencies.json","w") as file:
    json.dump(values,file)