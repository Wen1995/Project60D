# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: proto/building_data.proto

import sys
_b=sys.version_info[0]<3 and (lambda x:x) or (lambda x:x.encode('latin1'))
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
from google.protobuf import descriptor_pb2
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor.FileDescriptor(
  name='proto/building_data.proto',
  package='com.game.framework.resource.data',
  serialized_pb=_b('\n\x19proto/building_data.proto\x12 com.game.framework.resource.data\"\x8f\x05\n\x08\x42UILDING\x12\r\n\x02id\x18\x01 \x02(\x05:\x01\x30\x12\x14\n\tbldg_type\x18\x02 \x01(\x05:\x01\x30\x12\x13\n\x08\x62ldg_lvl\x18\x03 \x01(\x05:\x01\x30\x12\x13\n\tbldg_name\x18\x04 \x01(\t:\x00\x12\x11\n\x06in_use\x18\x05 \x01(\x05:\x01\x30\x12\x17\n\x0cmax_bldg_lvl\x18\x06 \x01(\x05:\x01\x30\x12\x15\n\x0bprefab_name\x18\x07 \x01(\t:\x00\x12\x13\n\ticon_name\x18\x08 \x01(\t:\x00\x12\x13\n\tbldg_info\x18\t \x01(\t:\x00\x12\x13\n\x08ui_order\x18\n \x01(\x05:\x01\x30\x12\x19\n\x0e\x62ldg_func_type\x18\x0b \x01(\x05:\x01\x30\x12\x12\n\x07\x63ult_pt\x18\x0c \x01(\x05:\x01\x30\x12\x16\n\x0bhr_slot_qty\x18\r \x01(\x05:\x01\x30\x12\x13\n\x08\x62uff_pro\x18\x0e \x01(\x05:\x01\x30\x12\x17\n\x0cmax_labor_uk\x18\x0f \x01(\x05:\x01\x30\x12\x1e\n\x14\x62ldg_func_table_name\x18\x10 \x01(\t:\x00\x12\x1d\n\x12\x62ldg_func_table_id\x18\x11 \x01(\x05:\x01\x30\x12\x1c\n\x11\x62ldg_strength_lim\x18\x12 \x01(\x05:\x01\x30\x12\x1c\n\x11\x62ldg_strength_add\x18\x13 \x01(\x05:\x01\x30\x12\x14\n\ttime_cost\x18\x14 \x01(\x05:\x01\x30\x12\x14\n\tgold_cost\x18\x15 \x01(\x05:\x01\x30\x12\x14\n\telec_cost\x18\x16 \x01(\x05:\x01\x30\x12I\n\ncost_table\x18\x17 \x03(\x0b\x32\x35.com.game.framework.resource.data.BUILDING.CostStruct\x1a\x35\n\nCostStruct\x12\x12\n\x07\x63ost_id\x18\x01 \x01(\x05:\x01\x30\x12\x13\n\x08\x63ost_qty\x18\x02 \x01(\x05:\x01\x30\"K\n\x0e\x42UILDING_ARRAY\x12\x39\n\x05items\x18\x01 \x03(\x0b\x32*.com.game.framework.resource.data.BUILDING')
)
_sym_db.RegisterFileDescriptor(DESCRIPTOR)




_BUILDING_COSTSTRUCT = _descriptor.Descriptor(
  name='CostStruct',
  full_name='com.game.framework.resource.data.BUILDING.CostStruct',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='cost_id', full_name='com.game.framework.resource.data.BUILDING.CostStruct.cost_id', index=0,
      number=1, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='cost_qty', full_name='com.game.framework.resource.data.BUILDING.CostStruct.cost_qty', index=1,
      number=2, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  options=None,
  is_extendable=False,
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=666,
  serialized_end=719,
)

_BUILDING = _descriptor.Descriptor(
  name='BUILDING',
  full_name='com.game.framework.resource.data.BUILDING',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='id', full_name='com.game.framework.resource.data.BUILDING.id', index=0,
      number=1, type=5, cpp_type=1, label=2,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='bldg_type', full_name='com.game.framework.resource.data.BUILDING.bldg_type', index=1,
      number=2, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='bldg_lvl', full_name='com.game.framework.resource.data.BUILDING.bldg_lvl', index=2,
      number=3, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='bldg_name', full_name='com.game.framework.resource.data.BUILDING.bldg_name', index=3,
      number=4, type=9, cpp_type=9, label=1,
      has_default_value=True, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='in_use', full_name='com.game.framework.resource.data.BUILDING.in_use', index=4,
      number=5, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='max_bldg_lvl', full_name='com.game.framework.resource.data.BUILDING.max_bldg_lvl', index=5,
      number=6, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='prefab_name', full_name='com.game.framework.resource.data.BUILDING.prefab_name', index=6,
      number=7, type=9, cpp_type=9, label=1,
      has_default_value=True, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='icon_name', full_name='com.game.framework.resource.data.BUILDING.icon_name', index=7,
      number=8, type=9, cpp_type=9, label=1,
      has_default_value=True, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='bldg_info', full_name='com.game.framework.resource.data.BUILDING.bldg_info', index=8,
      number=9, type=9, cpp_type=9, label=1,
      has_default_value=True, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='ui_order', full_name='com.game.framework.resource.data.BUILDING.ui_order', index=9,
      number=10, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='bldg_func_type', full_name='com.game.framework.resource.data.BUILDING.bldg_func_type', index=10,
      number=11, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='cult_pt', full_name='com.game.framework.resource.data.BUILDING.cult_pt', index=11,
      number=12, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='hr_slot_qty', full_name='com.game.framework.resource.data.BUILDING.hr_slot_qty', index=12,
      number=13, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='buff_pro', full_name='com.game.framework.resource.data.BUILDING.buff_pro', index=13,
      number=14, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='max_labor_uk', full_name='com.game.framework.resource.data.BUILDING.max_labor_uk', index=14,
      number=15, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='bldg_func_table_name', full_name='com.game.framework.resource.data.BUILDING.bldg_func_table_name', index=15,
      number=16, type=9, cpp_type=9, label=1,
      has_default_value=True, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='bldg_func_table_id', full_name='com.game.framework.resource.data.BUILDING.bldg_func_table_id', index=16,
      number=17, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='bldg_strength_lim', full_name='com.game.framework.resource.data.BUILDING.bldg_strength_lim', index=17,
      number=18, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='bldg_strength_add', full_name='com.game.framework.resource.data.BUILDING.bldg_strength_add', index=18,
      number=19, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='time_cost', full_name='com.game.framework.resource.data.BUILDING.time_cost', index=19,
      number=20, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='gold_cost', full_name='com.game.framework.resource.data.BUILDING.gold_cost', index=20,
      number=21, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='elec_cost', full_name='com.game.framework.resource.data.BUILDING.elec_cost', index=21,
      number=22, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='cost_table', full_name='com.game.framework.resource.data.BUILDING.cost_table', index=22,
      number=23, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
  ],
  extensions=[
  ],
  nested_types=[_BUILDING_COSTSTRUCT, ],
  enum_types=[
  ],
  options=None,
  is_extendable=False,
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=64,
  serialized_end=719,
)


_BUILDING_ARRAY = _descriptor.Descriptor(
  name='BUILDING_ARRAY',
  full_name='com.game.framework.resource.data.BUILDING_ARRAY',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='items', full_name='com.game.framework.resource.data.BUILDING_ARRAY.items', index=0,
      number=1, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  options=None,
  is_extendable=False,
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=721,
  serialized_end=796,
)

_BUILDING_COSTSTRUCT.containing_type = _BUILDING
_BUILDING.fields_by_name['cost_table'].message_type = _BUILDING_COSTSTRUCT
_BUILDING_ARRAY.fields_by_name['items'].message_type = _BUILDING
DESCRIPTOR.message_types_by_name['BUILDING'] = _BUILDING
DESCRIPTOR.message_types_by_name['BUILDING_ARRAY'] = _BUILDING_ARRAY

BUILDING = _reflection.GeneratedProtocolMessageType('BUILDING', (_message.Message,), dict(

  CostStruct = _reflection.GeneratedProtocolMessageType('CostStruct', (_message.Message,), dict(
    DESCRIPTOR = _BUILDING_COSTSTRUCT,
    __module__ = 'proto.building_data_pb2'
    # @@protoc_insertion_point(class_scope:com.game.framework.resource.data.BUILDING.CostStruct)
    ))
  ,
  DESCRIPTOR = _BUILDING,
  __module__ = 'proto.building_data_pb2'
  # @@protoc_insertion_point(class_scope:com.game.framework.resource.data.BUILDING)
  ))
_sym_db.RegisterMessage(BUILDING)
_sym_db.RegisterMessage(BUILDING.CostStruct)

BUILDING_ARRAY = _reflection.GeneratedProtocolMessageType('BUILDING_ARRAY', (_message.Message,), dict(
  DESCRIPTOR = _BUILDING_ARRAY,
  __module__ = 'proto.building_data_pb2'
  # @@protoc_insertion_point(class_scope:com.game.framework.resource.data.BUILDING_ARRAY)
  ))
_sym_db.RegisterMessage(BUILDING_ARRAY)


# @@protoc_insertion_point(module_scope)
