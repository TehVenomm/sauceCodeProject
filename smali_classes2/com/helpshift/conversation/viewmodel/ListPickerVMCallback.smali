.class public interface abstract Lcom/helpshift/conversation/viewmodel/ListPickerVMCallback;
.super Ljava/lang/Object;
.source "ListPickerVMCallback.java"


# virtual methods
.method public abstract handleOptionSelectedForPicker(Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;Z)V
.end method

.method public abstract hidePickerClearButton()V
.end method

.method public abstract showEmptyListPickerView()V
.end method

.method public abstract showPickerClearButton()V
.end method

.method public abstract updateListPickerOptions(Ljava/util/List;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/viewmodel/OptionUIModel;",
            ">;)V"
        }
    .end annotation
.end method
