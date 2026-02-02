const pharmacyData = {
  city: [
    { 
      name: "Paracetamol", 
      info: "500mg Tableta", 
      description: "Lehtëson dhimbjen e kokës, temperaturën dhe dhimbjet muskulare.",
      price: "350 ALL", 
      img: "assets/paracetamol.jpg",
      category: "pain-relief"
    },
    { 
      name: "Ibuprofen", 
      info: "400mg Tableta", 
      description: "Anti-inflamator për dhimbje dhe inflamacion.",
      price: "450 ALL", 
      img: "assets/ibuprofen.jpg",
      category: "pain-relief"
    },
    { 
      name: "Aspirin", 
      info: "100mg Tableta", 
      description: "Përdoret për hollimin e gjakut dhe mbrojtje kardiake.",
      price: "300 ALL", 
      img: "assets/1000654_1.avif",
      category: "cardiovascular"
    },
    { 
  name: "Gabapentin", 
  info: "300mg Kapsula", 
  description: "Përdoret për trajtimin e dhimbjeve neuropatike dhe epilepsisë.",
  price: "1200 ALL", 
  img: "assets/images (1).jepg",
  category: "neurology"
},
{ 
  name: "Carbamazepine", 
  info: "200mg Tableta", 
  description: "Trajtim për epilepsi dhe çrregullime nervore.",
  price: "950 ALL", 
  img: "assets/teg.jepg",
  category: "neurology"
},
{ 
  name: "Coldrex", 
  info: "Tableta", 
  description: "Lehtëson simptomat e gripit dhe ftohjes.",
  price: "600 ALL", 
  img: "assets/coldrex.jepg",
  category: "cold-flu"
},
{ 
  name: "Fluimucil", 
  info: "600mg", 
  description: "Ndihmon në hollimin e sekrecioneve dhe kollës.",
  price: "700 ALL", 
  img: "assets/fluimucil.jpg",
  category: "cold-flu"
}
,
    { 
      name: "Vitamin C", 
      info: "1000mg", 
      description: "Forcon sistemin imunitar.",
      price: "500 ALL", 
      img: "assets/images.jpeg",
      category: "immunity"
    }
  ],

  healthplus: [
    { 
      name: "Insulin", 
      info: "Injeksion", 
      description: "Rregullon nivelin e sheqerit në gjak për diabetikët.",
      price: "2500 ALL", 
      img: "assets/insulin.jpg",
      category: "diabetes"
    },
    { 
      name: "Amoxicillin", 
      info: "500mg Kapsula", 
      description: "Antibiotik për infeksione bakteriale.",
      price: "800 ALL", 
      img: "assets/amoxicillin.jpg",
      category: "antibiotics"
    },
    { 
      name: "Metformin", 
      info: "500mg Tableta", 
      description: "Përdoret për trajtimin e diabetit tip 2.",
      price: "700 ALL", 
      img: "assets/metformin.jpg",
      category: "diabetes"
    },
    { 
      name: "Rapidol S", 
      info: "500mg", 
      description: "Qetësues për dhimbje të lehta dhe mesatare.",
      price: "600 ALL", 
      img: "assets/rapidolS.jpg",
      category: "pain-relief"
    }
  ],

  vita: [
    { 
      name: "Cough Syrup", 
      info: "Shurup", 
      description: "Qetëson kollën dhe irritimin e fytit.",
      price: "750 ALL", 
      img: "assets/COUGH.png",
      category: "cold-flu"
    },
    { 
      name: "Tachipirina", 
      info: "Shurup", 
      description: "Për temperaturë tek fëmijët dhe të rriturit.",
      price: "900 ALL", 
      img: "assets/tachipirina.jpg",
      category: "cold-flu"
    },
    { 
      name: "Panadol", 
      info: "500mg", 
      description: "Lehtëson dhimbjet dhe ul temperaturën.",
      price: "400 ALL", 
      img: "assets/panadoladvance-Photoroom.jpg",
      category: "pain-relief"
    }
  ],

  greencross: [
    { 
      name: "Ibuprofen", 
      info: "400mg", 
      description: "Për dhimbje muskulare dhe inflamacion.",
      price: "480 ALL", 
      img: "assets/ibuprofen.jpg",
      category: "pain-relief"
    },
    { 
      name: "Amoxicillin", 
      info: "250mg", 
      description: "Antibiotik për infeksione të ndryshme.",
      price: "750 ALL", 
      img: "assets/amoxicillin.jpg",
      category: "antibiotics"
    },
    { 
      name: "Augmentin", 
      info: "625mg", 
      description: "Antibiotik i kombinuar për infeksione të forta.",
      price: "1200 ALL", 
      img: "assets/augmentin.jpeg",
      category: "antibiotics"
    }
  ],

  medicare: [
    { 
      name: "Insulin", 
      info: "Injeksion", 
      description: "Trajtim për diabet tip 1 dhe 2.",
      price: "2400 ALL", 
      img: "assets/insulin.jpg",
      category: "injections"
    },
    { 
      name: "Vitamin D3", 
      info: "1000 IU", 
      description: "Forcon kockat dhe sistemin imunitar.",
      price: "600 ALL", 
      img: "assets/paracetamol.jpg",
      category: "immunity"
    },
    { 
      name: "Omega 3", 
      info: "Kapsula", 
      description: "Mbështet shëndetin e zemrës.",
      price: "950 ALL", 
      img: "assets/omega3.webp",
      category: "cardiovascular"
    }
  ],

  family: [
    { 
      name: "Rapidol S", 
      info: "500mg", 
      description: "Qetësues për dhimbje koke dhe temperature.",
      price: "650 ALL", 
      img: "assets/rapidolS.jpg",
      category: "pain-relief"
    },
    { 
      name: "Tachipirina", 
      info: "Shurup", 
      description: "Ul temperaturën tek fëmijët.",
      price: "950 ALL", 
      img: "assets/tachipirina.jpg",
      category: "cold-flu"
    },
    { 
      name: "Bronchicum", 
      info: "Shurup", 
      description: "Për kollë dhe probleme respiratore.",
      price: "850 ALL", 
      img: "assets/tachipirina.jpg",
      category: "respiratory"
    }
  ],

  careplus: [
    { 
      name: "Amoxicillin", 
      info: "500mg", 
      description: "Antibiotik për infeksione bakteriale.",
      price: "850 ALL", 
      img: "assets/amoxicillin.jpg",
      category: "antibiotics"
    },
    { 
      name: "Paracetamol", 
      info: "500mg", 
      description: "Për dhimbje dhe temperaturë.",
      price: "350 ALL", 
      img: "assets/paracetamol.jpg",
      category: "pain-relief"
    },
    { 
      name: "Nurofen", 
      info: "200mg", 
      description: "Anti-inflamator për dhimbje.",
      price: "500 ALL", 
      img: "assets/ibuprofen.jpg",
      category: "pain-relief"
    }
  ],

  rapid: [
    { 
      name: "Ibuprofen", 
      info: "200mg", 
      description: "Qetësues dhe anti-inflamator.",
      price: "400 ALL", 
      img: "assets/ibuprofen.jpg",
      category: "pain-relief"
    },
    { 
      name: "Insulin", 
      info: "Injeksion", 
      description: "Rregullon nivelin e glukozës në gjak.",
      price: "2600 ALL", 
      img: "assets/insulin.jpg",
      category: "injections"
    },
    { 
      name: "Voltaren", 
      info: "Gel", 
      description: "Për dhimbje muskulare dhe nyjesh.",
      price: "700 ALL", 
      img: "assets/ibuprofen.jpg",
      category: "pain-relief"
    }
  ]
};
