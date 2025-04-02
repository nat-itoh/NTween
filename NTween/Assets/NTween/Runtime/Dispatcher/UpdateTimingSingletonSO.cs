using UnityEngine;

namespace NTween.Internal {

    internal abstract class UpdateTimingSingletonSO<TSystem> : ScriptableObject
        where TSystem : UpdateTimingSingletonSO<TSystem> {

        /// <summary>
        /// �X�V�^�C�~���O�D
        /// </summary>
        public UpdateTiming Timing { get; private set; }

        /// <summary>
        /// �C���X�^���X���������ꂽ�Ƃ��̏����D
        /// </summary>
        protected abstract void OnCreate(UpdateTiming timing);

        /// <summary>
        /// �A�v�����I�����鎞�̏����D
        /// This is to handle EnterPlayMode.
        /// </summary>
        private void OnQuit() {
            Application.quitting -= OnQuit;
            if (Application.isPlaying) {
                Destroy(this);
            } else {
                DestroyImmediate(this);
            }
        }


        /// ----------------------------------------------------------------------------
        #region Static

        /// <summary>
        /// <see cref="UpdateTiming"/>�̊e�^�C�~���O���T�|�[�g���邽�߂̃C���X�^���X�D
        /// </summary>
        private static readonly TSystem[] Instance = new TSystem[3];

        /// <summary>
        /// �C���X�^���X�������ς݂��m�F����D
        /// </summary>
        public static bool IsCreated(UpdateTiming timing) => Instance[(int)timing] != null;

        /// <summary>
        /// �w�肵���^�C�~���O�̃C���X�^���X���擾����D�܂���������Ă��Ȃ��ꍇ�́C��������D
        /// </summary>
        /// <param name="timing">�X�V�^�C�~���O�D</param>
        /// <returns>�C���X�^���X�D</returns>
        public static TSystem GetInstance(UpdateTiming timing) {
            var index = (int)timing;
            if (IsCreated(timing)) return Instance[index];

            var instance = CreateInstance<TSystem>();
            instance.Timing = timing;
            instance.OnCreate(timing);
            Application.quitting += instance.OnQuit;

            Instance[index] = instance;
            return instance;
        }
        #endregion
    }
}
