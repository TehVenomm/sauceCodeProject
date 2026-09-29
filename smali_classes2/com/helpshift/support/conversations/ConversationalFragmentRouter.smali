.class public interface abstract Lcom/helpshift/support/conversations/ConversationalFragmentRouter;
.super Ljava/lang/Object;
.source "ConversationalFragmentRouter.java"

# interfaces
.implements Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;


# virtual methods
.method public abstract handleOptionSelectedForPicker(Lcom/helpshift/conversation/viewmodel/OptionUIModel;Z)V
.end method

.method public abstract onAuthenticationFailure()V
.end method

.method public abstract onListPickerSearchQueryChange(Ljava/lang/String;)V
.end method

.method public abstract onSendButtonClick()V
.end method

.method public abstract onSkipClick()V
.end method

.method public abstract onTextChanged(Ljava/lang/CharSequence;III)V
.end method

.method public abstract openFreshConversationScreen(Ljava/util/Map;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Boolean;",
            ">;)V"
        }
    .end annotation
.end method

.method public abstract resetToolbarImportanceForAccessibility()V
.end method

.method public abstract setToolbarImportanceForAccessibility(I)V
.end method
