.class Lcom/helpshift/conversation/viewmodel/ListPickerVM$4;
.super Lcom/helpshift/common/domain/F;
.source "ListPickerVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/ListPickerVM;->notifyShowEmptyListPickerView()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/ListPickerVM;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/ListPickerVM;)V
    .locals 0

    .line 130
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ListPickerVM$4;->this$0:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 1

    .line 133
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ListPickerVM$4;->this$0:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

    invoke-static {v0}, Lcom/helpshift/conversation/viewmodel/ListPickerVM;->access$400(Lcom/helpshift/conversation/viewmodel/ListPickerVM;)Lcom/helpshift/conversation/viewmodel/ListPickerVMCallback;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 134
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ListPickerVM$4;->this$0:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

    invoke-static {v0}, Lcom/helpshift/conversation/viewmodel/ListPickerVM;->access$400(Lcom/helpshift/conversation/viewmodel/ListPickerVM;)Lcom/helpshift/conversation/viewmodel/ListPickerVMCallback;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/conversation/viewmodel/ListPickerVMCallback;->showEmptyListPickerView()V

    :cond_0
    return-void
.end method
