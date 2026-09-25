.class Lcom/helpshift/conversation/viewmodel/ListPickerVM$5;
.super Lcom/helpshift/common/domain/F;
.source "ListPickerVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/ListPickerVM;->notifyListPickerOptions(Ljava/util/List;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

.field final synthetic val$optionUIModels:Ljava/util/List;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/ListPickerVM;Ljava/util/List;)V
    .locals 0

    .line 141
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ListPickerVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

    iput-object p2, p0, Lcom/helpshift/conversation/viewmodel/ListPickerVM$5;->val$optionUIModels:Ljava/util/List;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 2

    .line 144
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ListPickerVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

    invoke-static {v0}, Lcom/helpshift/conversation/viewmodel/ListPickerVM;->access$400(Lcom/helpshift/conversation/viewmodel/ListPickerVM;)Lcom/helpshift/conversation/viewmodel/ListPickerVMCallback;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 145
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ListPickerVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

    invoke-static {v0}, Lcom/helpshift/conversation/viewmodel/ListPickerVM;->access$400(Lcom/helpshift/conversation/viewmodel/ListPickerVM;)Lcom/helpshift/conversation/viewmodel/ListPickerVMCallback;

    move-result-object v0

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ListPickerVM$5;->val$optionUIModels:Ljava/util/List;

    invoke-interface {v0, v1}, Lcom/helpshift/conversation/viewmodel/ListPickerVMCallback;->updateListPickerOptions(Ljava/util/List;)V

    :cond_0
    return-void
.end method
