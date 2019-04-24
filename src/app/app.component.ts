import { Component, OnInit } from '@angular/core';
import { ChartData, Currency } from './models/chart-data.model';
import { PromiseData } from './models/promise-data.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  public chartData: ChartData[] = [{
    label: 'Курс доллара (UAH за 1 USD)',
    invert: false,
    initialData: {
      date: '01.06.2019',
      currency: Currency.UAH,
      value: '27,10'
    },
    currentData: {
      date: '01.07.2019',
      currency: Currency.UAH,
      value: '27,20'
    }
  }, {
    label: 'Курс доллара (UAH за 1 USD)',
    invert: false,
    initialData: {
      date: '01.06.2019',
      currency: Currency.UAH,
      value: '25,10'
    },
    currentData: {
      date: '01.07.2019',
      currency: Currency.UAH,
      value: '24,20'
    }
  }, {
    label: 'Курс доллара (UAH за 1 USD)',
    invert: false,
    initialData: {
      date: '01.06.2019',
      currency: Currency.UAH,
      value: '27,10'
    },
    currentData: {
      date: '01.07.2019',
      currency: Currency.UAH,
      value: '24,20'
    }
  }, {
    label: 'Курс доллара (UAH за 1 USD)',
    invert: false,
    initialData: {
      date: '01.06.2019',
      currency: Currency.UAH,
      value: '27,10'
    },
    currentData: {
      date: '01.07.2019',
      currency: Currency.UAH,
      value: '27,20'
    }
  }, {
    label: 'Курс доллара (UAH за 1 USD)',
    invert: false,
    initialData: {
      date: '01.06.2019',
      currency: Currency.UAH,
      value: '26,10'
    },
    currentData: {
      date: '01.07.2019',
      currency: Currency.UAH,
      value: '27,20'
    }
  }, {
    label: 'Курс доллара (UAH за 1 USD)',
    invert: true,
    initialData: {
      date: '01.06.2019',
      currency: Currency.UAH,
      value: '26,10'
    },
    currentData: {
      date: '01.07.2019',
      currency: Currency.UAH,
      value: '27,20'
    }
  }, {
    label: 'Курс доллара (UAH за 1 USD)',
    invert: false,
    initialData: {
      date: '01.06.2019',
      currency: Currency.UAH,
      value: '27,10'
    },
    currentData: {
      date: '01.07.2019',
      currency: Currency.UAH,
      value: '26,20'
    }
  }, {
    label: 'Курс доллара (UAH за 1 USD)',
    invert: true,
    initialData: {
      date: '01.06.2019',
      currency: Currency.UAH,
      value: '27,10'
    },
    currentData: {
      date: '01.07.2019',
      currency: Currency.UAH,
      value: '26,20'
    }
  }];

  public promiseData: PromiseData[] = [{
    description: 'Знизити вартість комунальних послуг.',
    completed: false
  }, {
    description: 'Прийняття законопроектів про імпічмент президента, скасування недоторканності депутатів і суддів.',
    completed: false
  }, {
    description: 'Прийняти новий виборчий кодекс.',
    completed: true
  }, {
    // tslint:disable-next-line: max-line-length
    description: 'Ввести одноразову "нульову декларацію" для бізнесу": кожен бізнесмен за 5% зможе задекларувати і легалізувати свої доходи.',
    completed: false
  }, {
    // tslint:disable-next-line: max-line-length
    description: 'Кардинально переглянути привілеї президента (продати державні літаки, виїхати з Адміністрації президента на Банковій, відмовитися від кортежів та перекривання доріг).',
    completed: false
  }, {
    // tslint:disable-next-line: max-line-length
    description: 'Перезапустити реформу судів та силових структур, а також антикорупційної інфраструктури, створити Службу фінансових розслідувань.',
    completed: false
  }, {
    description: 'Вийти з бізнесу.',
    completed: false
  }, {
    description: 'Закінчити війну.',
    completed: false
  }, {
    description: 'Домогтися розширення Нормандськогоформату (запросивши до участі Велику Британію і США).',
    completed: false
  }, {
    description: 'Заробітна плата вчителям $ 4,000.',
    completed: true
  }, {
    description: 'Не йти на компроміси в питаннях про приналежність Україні Криму та окупованих територій Донецької і Луганської областей.',
    completed: false
  }, {
    // tslint:disable-next-line: max-line-length
    description: 'Перезапустити реформу судів та силових структур, а також нтикорупційної інфраструктури, створити Службу фінансових розслідувань.',
    completed: false
  }, {
    description: 'Прийняття пакету законопроектів про народовладдя.',
    completed: false
  }];

  public breakpoint = 2;

  public ngOnInit() {
    this.breakpoint = (window.innerWidth <= 600) ? 1 : 2;
  }

  public onResize(event) {
    this.breakpoint = (event.target.innerWidth <= 600) ? 1 : 2;
  }

}
