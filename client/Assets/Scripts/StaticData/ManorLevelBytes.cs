// Generated by ProtoGen, Version=2.4.1.521, Culture=neutral, PublicKeyToken=null.  DO NOT EDIT!
#pragma warning disable
#region Designer generated code

using pb = global::Google.ProtocolBuffers;
using pbc = global::Google.ProtocolBuffers.Collections;
using pbd = global::Google.ProtocolBuffers.Descriptors;
using scg = global::System.Collections.Generic;
namespace com.game.framework.resource.data {
  
  public static partial class ManorLevelBytes {
  
    #region Extension registration
    public static void RegisterAllExtensions(pb::ExtensionRegistry registry) {
    }
    #endregion
    #region Static variables
    #endregion
    #region Extensions
    internal static readonly object Descriptor;
    static ManorLevelBytes() {
      Descriptor = null;
    }
    #endregion
    
  }
  #region Messages
  public sealed partial class MANOR_LEVEL : pb::GeneratedMessageLite<MANOR_LEVEL, MANOR_LEVEL.Builder> {
    private MANOR_LEVEL() { }
    private static readonly MANOR_LEVEL defaultInstance = new MANOR_LEVEL().MakeReadOnly();
    private static readonly string[] _mANORLEVELFieldNames = new string[] { "manor_cap", "manor_level" };
    private static readonly uint[] _mANORLEVELFieldTags = new uint[] { 16, 8 };
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MANOR_LEVEL DefaultInstance {
      get { return defaultInstance; }
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override MANOR_LEVEL DefaultInstanceForType {
      get { return DefaultInstance; }
    }
    
    protected override MANOR_LEVEL ThisMessage {
      get { return this; }
    }
    
    #if UNITY_EDITOR
    [pb.FieldNumber]
    #endif//
    public const int ManorLevelFieldNumber = 1;
    private bool hasManorLevel;
    private int manorLevel_;
    public bool HasManorLevel {
      get { return hasManorLevel; }
    }
    public int ManorLevel {
      get { return manorLevel_; }
    }
    
    #if UNITY_EDITOR
    [pb.FieldNumber]
    #endif//
    public const int ManorCapFieldNumber = 2;
    private bool hasManorCap;
    private int manorCap_;
    public bool HasManorCap {
      get { return hasManorCap; }
    }
    public int ManorCap {
      get { return manorCap_; }
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override bool IsInitialized {
      get {
        if (!hasManorLevel) return false;
        return true;
      }
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override void WriteTo(pb::ICodedOutputStream output) {
      int size = SerializedSize;
      string[] field_names = _mANORLEVELFieldNames;
      if (hasManorLevel) {
        output.WriteInt32(1, field_names[1], ManorLevel);
      }
      if (hasManorCap) {
        output.WriteInt32(2, field_names[0], ManorCap);
      }
    }
    
    private int memoizedSerializedSize = -1;
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override int SerializedSize {
      get {
        int size = memoizedSerializedSize;
        if (size != -1) return size;
        
        size = 0;
        if (hasManorLevel) {
          size += pb::CodedOutputStream.ComputeInt32Size(1, ManorLevel);
        }
        if (hasManorCap) {
          size += pb::CodedOutputStream.ComputeInt32Size(2, ManorCap);
        }
        memoizedSerializedSize = size;
        return size;
      }
    }
    
    #region Lite runtime methods
    public override int GetHashCode() {
      int hash = GetType().GetHashCode();
      if (hasManorLevel) hash ^= manorLevel_.GetHashCode();
      if (hasManorCap) hash ^= manorCap_.GetHashCode();
      return hash;
    }
    
    public override bool Equals(object obj) {
      MANOR_LEVEL other = obj as MANOR_LEVEL;
      if (other == null) return false;
      if (hasManorLevel != other.hasManorLevel || (hasManorLevel && !manorLevel_.Equals(other.manorLevel_))) return false;
      if (hasManorCap != other.hasManorCap || (hasManorCap && !manorCap_.Equals(other.manorCap_))) return false;
      return true;
    }
    
    #endregion
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MANOR_LEVEL ParseFrom(pb::ByteString data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MANOR_LEVEL ParseFrom(pb::ByteString data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MANOR_LEVEL ParseFrom(byte[] data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MANOR_LEVEL ParseFrom(byte[] data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MANOR_LEVEL ParseFrom(global::System.IO.Stream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MANOR_LEVEL ParseFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MANOR_LEVEL ParseDelimitedFrom(global::System.IO.Stream input) {
      return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MANOR_LEVEL ParseDelimitedFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MANOR_LEVEL ParseFrom(pb::ICodedInputStream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MANOR_LEVEL ParseFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    private MANOR_LEVEL MakeReadOnly() {
      return this;
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static Builder CreateBuilder() { return new Builder(); }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override Builder ToBuilder() { return CreateBuilder(this); }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override Builder CreateBuilderForType() { return new Builder(); }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static Builder CreateBuilder(MANOR_LEVEL prototype) {
      return new Builder(prototype);
    }
    
    public sealed partial class Builder : pb::GeneratedBuilderLite<MANOR_LEVEL, Builder> {
      protected override Builder ThisBuilder {
        get { return this; }
      }
      public Builder() {
        result = DefaultInstance;
        resultIsReadOnly = true;
      }
      internal Builder(MANOR_LEVEL cloneFrom) {
        result = cloneFrom;
        resultIsReadOnly = true;
      }
      
      private bool resultIsReadOnly;
      private MANOR_LEVEL result;
      
      private MANOR_LEVEL PrepareBuilder() {
        if (resultIsReadOnly) {
          MANOR_LEVEL original = result;
          result = new MANOR_LEVEL();
          resultIsReadOnly = false;
          MergeFrom(original);
        }
        return result;
      }
      
      public override bool IsInitialized {
        get { return result.IsInitialized; }
      }
      
      protected override MANOR_LEVEL MessageBeingBuilt {
        get { return PrepareBuilder(); }
      }
      
      public override Builder Clear() {
        result = DefaultInstance;
        resultIsReadOnly = true;
        return this;
      }
      
      public override Builder Clone() {
        if (resultIsReadOnly) {
          return new Builder(result);
        } else {
          return new Builder().MergeFrom(result);
        }
      }
      
      public override MANOR_LEVEL DefaultInstanceForType {
        get { return global::com.game.framework.resource.data.MANOR_LEVEL.DefaultInstance; }
      }
      
      public override MANOR_LEVEL BuildPartial() {
        if (resultIsReadOnly) {
          return result;
        }
        resultIsReadOnly = true;
        return result.MakeReadOnly();
      }
      
      public override Builder MergeFrom(pb::IMessageLite other) {
        if (other is MANOR_LEVEL) {
          return MergeFrom((MANOR_LEVEL) other);
        } else {
          base.MergeFrom(other);
          return this;
        }
      }
      
      public override Builder MergeFrom(MANOR_LEVEL other) {
        if (other == global::com.game.framework.resource.data.MANOR_LEVEL.DefaultInstance) return this;
        PrepareBuilder();
        if (other.HasManorLevel) {
          ManorLevel = other.ManorLevel;
        }
        if (other.HasManorCap) {
          ManorCap = other.ManorCap;
        }
        return this;
      }
      
      public override Builder MergeFrom(pb::ICodedInputStream input) {
        return MergeFrom(input, pb::ExtensionRegistry.Empty);
      }
      
      public override Builder MergeFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
        PrepareBuilder();
        uint tag;
        string field_name;
        while (input.ReadTag(out tag, out field_name)) {
          if(tag == 0 && field_name != null) {
            int field_ordinal = global::System.Array.BinarySearch(_mANORLEVELFieldNames, field_name, global::System.StringComparer.Ordinal);
            if(field_ordinal >= 0)
              tag = _mANORLEVELFieldTags[field_ordinal];
            else {
              ParseUnknownField(input, extensionRegistry, tag, field_name);
              continue;
            }
          }
          switch (tag) {
            case 0: {
              throw pb::InvalidProtocolBufferException.InvalidTag();
            }
            default: {
              if (pb::WireFormat.IsEndGroupTag(tag)) {
                return this;
              }
              ParseUnknownField(input, extensionRegistry, tag, field_name);
              break;
            }
            case 8: {
              result.hasManorLevel = input.ReadInt32(ref result.manorLevel_);
              break;
            }
            case 16: {
              result.hasManorCap = input.ReadInt32(ref result.manorCap_);
              break;
            }
          }
        }
        
        return this;
      }
      
      
      public bool HasManorLevel {
        get { return result.hasManorLevel; }
      }
      public int ManorLevel {
        get { return result.ManorLevel; }
        set { SetManorLevel(value); }
      }
      public Builder SetManorLevel(int value) {
        PrepareBuilder();
        result.hasManorLevel = true;
        result.manorLevel_ = value;
        return this;
      }
      public Builder ClearManorLevel() {
        PrepareBuilder();
        result.hasManorLevel = false;
        result.manorLevel_ = 0;
        return this;
      }
      
      public bool HasManorCap {
        get { return result.hasManorCap; }
      }
      public int ManorCap {
        get { return result.ManorCap; }
        set { SetManorCap(value); }
      }
      public Builder SetManorCap(int value) {
        PrepareBuilder();
        result.hasManorCap = true;
        result.manorCap_ = value;
        return this;
      }
      public Builder ClearManorCap() {
        PrepareBuilder();
        result.hasManorCap = false;
        result.manorCap_ = 0;
        return this;
      }
    }
    static MANOR_LEVEL() {
      object.ReferenceEquals(global::com.game.framework.resource.data.ManorLevelBytes.Descriptor, null);
    }
  }
  
  public sealed partial class MANOR_LEVEL_ARRAY : pb::GeneratedMessageLite<MANOR_LEVEL_ARRAY, MANOR_LEVEL_ARRAY.Builder> {
    private MANOR_LEVEL_ARRAY() { }
    private static readonly MANOR_LEVEL_ARRAY defaultInstance = new MANOR_LEVEL_ARRAY().MakeReadOnly();
    private static readonly string[] _mANORLEVELARRAYFieldNames = new string[] { "items" };
    private static readonly uint[] _mANORLEVELARRAYFieldTags = new uint[] { 10 };
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MANOR_LEVEL_ARRAY DefaultInstance {
      get { return defaultInstance; }
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override MANOR_LEVEL_ARRAY DefaultInstanceForType {
      get { return DefaultInstance; }
    }
    
    protected override MANOR_LEVEL_ARRAY ThisMessage {
      get { return this; }
    }
    
    #if UNITY_EDITOR
    [pb.FieldNumber]
    #endif//
    public const int ItemsFieldNumber = 1;
    private pbc::PopsicleList<global::com.game.framework.resource.data.MANOR_LEVEL> items_ = new pbc::PopsicleList<global::com.game.framework.resource.data.MANOR_LEVEL>();
    public scg::IList<global::com.game.framework.resource.data.MANOR_LEVEL> ItemsList {
      get { return items_; }
    }
    public int ItemsCount {
      get { return items_.Count; }
    }
    public global::com.game.framework.resource.data.MANOR_LEVEL GetItems(int index) {
      return items_[index];
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override bool IsInitialized {
      get {
        foreach (global::com.game.framework.resource.data.MANOR_LEVEL element in ItemsList) {
          if (!element.IsInitialized) return false;
        }
        return true;
      }
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override void WriteTo(pb::ICodedOutputStream output) {
      int size = SerializedSize;
      string[] field_names = _mANORLEVELARRAYFieldNames;
      if (items_.Count > 0) {
        output.WriteMessageArray(1, field_names[0], items_);
      }
    }
    
    private int memoizedSerializedSize = -1;
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override int SerializedSize {
      get {
        int size = memoizedSerializedSize;
        if (size != -1) return size;
        
        size = 0;
        foreach (global::com.game.framework.resource.data.MANOR_LEVEL element in ItemsList) {
          size += pb::CodedOutputStream.ComputeMessageSize(1, element);
        }
        memoizedSerializedSize = size;
        return size;
      }
    }
    
    #region Lite runtime methods
    public override int GetHashCode() {
      int hash = GetType().GetHashCode();
      foreach(global::com.game.framework.resource.data.MANOR_LEVEL i in items_)
        hash ^= i.GetHashCode();
      return hash;
    }
    
    public override bool Equals(object obj) {
      MANOR_LEVEL_ARRAY other = obj as MANOR_LEVEL_ARRAY;
      if (other == null) return false;
      if(items_.Count != other.items_.Count) return false;
      for(int ix=0; ix < items_.Count; ix++)
        if(!items_[ix].Equals(other.items_[ix])) return false;
      return true;
    }
    
    #endregion
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MANOR_LEVEL_ARRAY ParseFrom(pb::ByteString data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MANOR_LEVEL_ARRAY ParseFrom(pb::ByteString data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MANOR_LEVEL_ARRAY ParseFrom(byte[] data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MANOR_LEVEL_ARRAY ParseFrom(byte[] data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MANOR_LEVEL_ARRAY ParseFrom(global::System.IO.Stream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MANOR_LEVEL_ARRAY ParseFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MANOR_LEVEL_ARRAY ParseDelimitedFrom(global::System.IO.Stream input) {
      return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MANOR_LEVEL_ARRAY ParseDelimitedFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MANOR_LEVEL_ARRAY ParseFrom(pb::ICodedInputStream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MANOR_LEVEL_ARRAY ParseFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    private MANOR_LEVEL_ARRAY MakeReadOnly() {
      items_.MakeReadOnly();
      return this;
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static Builder CreateBuilder() { return new Builder(); }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override Builder ToBuilder() { return CreateBuilder(this); }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override Builder CreateBuilderForType() { return new Builder(); }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static Builder CreateBuilder(MANOR_LEVEL_ARRAY prototype) {
      return new Builder(prototype);
    }
    
    public sealed partial class Builder : pb::GeneratedBuilderLite<MANOR_LEVEL_ARRAY, Builder> {
      protected override Builder ThisBuilder {
        get { return this; }
      }
      public Builder() {
        result = DefaultInstance;
        resultIsReadOnly = true;
      }
      internal Builder(MANOR_LEVEL_ARRAY cloneFrom) {
        result = cloneFrom;
        resultIsReadOnly = true;
      }
      
      private bool resultIsReadOnly;
      private MANOR_LEVEL_ARRAY result;
      
      private MANOR_LEVEL_ARRAY PrepareBuilder() {
        if (resultIsReadOnly) {
          MANOR_LEVEL_ARRAY original = result;
          result = new MANOR_LEVEL_ARRAY();
          resultIsReadOnly = false;
          MergeFrom(original);
        }
        return result;
      }
      
      public override bool IsInitialized {
        get { return result.IsInitialized; }
      }
      
      protected override MANOR_LEVEL_ARRAY MessageBeingBuilt {
        get { return PrepareBuilder(); }
      }
      
      public override Builder Clear() {
        result = DefaultInstance;
        resultIsReadOnly = true;
        return this;
      }
      
      public override Builder Clone() {
        if (resultIsReadOnly) {
          return new Builder(result);
        } else {
          return new Builder().MergeFrom(result);
        }
      }
      
      public override MANOR_LEVEL_ARRAY DefaultInstanceForType {
        get { return global::com.game.framework.resource.data.MANOR_LEVEL_ARRAY.DefaultInstance; }
      }
      
      public override MANOR_LEVEL_ARRAY BuildPartial() {
        if (resultIsReadOnly) {
          return result;
        }
        resultIsReadOnly = true;
        return result.MakeReadOnly();
      }
      
      public override Builder MergeFrom(pb::IMessageLite other) {
        if (other is MANOR_LEVEL_ARRAY) {
          return MergeFrom((MANOR_LEVEL_ARRAY) other);
        } else {
          base.MergeFrom(other);
          return this;
        }
      }
      
      public override Builder MergeFrom(MANOR_LEVEL_ARRAY other) {
        if (other == global::com.game.framework.resource.data.MANOR_LEVEL_ARRAY.DefaultInstance) return this;
        PrepareBuilder();
        if (other.items_.Count != 0) {
          result.items_.Add(other.items_);
        }
        return this;
      }
      
      public override Builder MergeFrom(pb::ICodedInputStream input) {
        return MergeFrom(input, pb::ExtensionRegistry.Empty);
      }
      
      public override Builder MergeFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
        PrepareBuilder();
        uint tag;
        string field_name;
        while (input.ReadTag(out tag, out field_name)) {
          if(tag == 0 && field_name != null) {
            int field_ordinal = global::System.Array.BinarySearch(_mANORLEVELARRAYFieldNames, field_name, global::System.StringComparer.Ordinal);
            if(field_ordinal >= 0)
              tag = _mANORLEVELARRAYFieldTags[field_ordinal];
            else {
              ParseUnknownField(input, extensionRegistry, tag, field_name);
              continue;
            }
          }
          switch (tag) {
            case 0: {
              throw pb::InvalidProtocolBufferException.InvalidTag();
            }
            default: {
              if (pb::WireFormat.IsEndGroupTag(tag)) {
                return this;
              }
              ParseUnknownField(input, extensionRegistry, tag, field_name);
              break;
            }
            case 10: {
              input.ReadMessageArray(tag, field_name, result.items_, global::com.game.framework.resource.data.MANOR_LEVEL.DefaultInstance, extensionRegistry);
              break;
            }
          }
        }
        
        return this;
      }
      
      
      public pbc::IPopsicleList<global::com.game.framework.resource.data.MANOR_LEVEL> ItemsList {
        get { return PrepareBuilder().items_; }
      }
      public int ItemsCount {
        get { return result.ItemsCount; }
      }
      public global::com.game.framework.resource.data.MANOR_LEVEL GetItems(int index) {
        return result.GetItems(index);
      }
      public Builder SetItems(int index, global::com.game.framework.resource.data.MANOR_LEVEL value) {
        pb::ThrowHelper.ThrowIfNull(value, "value");
        PrepareBuilder();
        result.items_[index] = value;
        return this;
      }
      public Builder SetItems(int index, global::com.game.framework.resource.data.MANOR_LEVEL.Builder builderForValue) {
        pb::ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
        PrepareBuilder();
        result.items_[index] = builderForValue.Build();
        return this;
      }
      public Builder AddItems(global::com.game.framework.resource.data.MANOR_LEVEL value) {
        pb::ThrowHelper.ThrowIfNull(value, "value");
        PrepareBuilder();
        result.items_.Add(value);
        return this;
      }
      public Builder AddItems(global::com.game.framework.resource.data.MANOR_LEVEL.Builder builderForValue) {
        pb::ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
        PrepareBuilder();
        result.items_.Add(builderForValue.Build());
        return this;
      }
      public Builder AddRangeItems(scg::IEnumerable<global::com.game.framework.resource.data.MANOR_LEVEL> values) {
        PrepareBuilder();
        result.items_.Add(values);
        return this;
      }
      public Builder ClearItems() {
        PrepareBuilder();
        result.items_.Clear();
        return this;
      }
    }
    static MANOR_LEVEL_ARRAY() {
      object.ReferenceEquals(global::com.game.framework.resource.data.ManorLevelBytes.Descriptor, null);
    }
  }
  
  #endregion
  
}

#endregion Designer generated code
#pragma warning restore
